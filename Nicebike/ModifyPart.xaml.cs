namespace Nicebike;
using Nicebike.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Reflection.Metadata;

public partial class ModifyPart : ContentPage
{
    Part part;
	int IdPart;
    int i = 0;
    List<String> suppliersnames = new List<String>();
    List<Supplier> suppliers = new List<Supplier>();
    public ModifyPart(Part part)
    {
        InitializeComponent();
        this.part = part;
        IdPart = part.id;

        // Créer une instance de la classe SupplierManagement
        SupplierManagement supplierManagement = new SupplierManagement();

        // Récupérer la liste des fournisseurs à partir de la base de données
        suppliers = supplierManagement.GetAllSuppliers();

        foreach (Supplier supplier in suppliers)
        {
            suppliersnames.Add(suppliers[i].name);
            i = i + 1;
        }

        // Assigner la liste des fournisseurs à la source de données du ListView
        supplierPickerModify.ItemsSource = suppliersnames;

        // Définir le contexte de liaison des données
        BindingContext = part;
    }

    public async void modifyClickedPart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntryModify");
        Entry description = this.FindByName<Entry>("descriptionEntryModify");
        Entry quantity = this.FindByName<Entry>("quantityEntryModify");
        Entry threshold = this.FindByName<Entry>("thresholdEntryModify");
        Picker supplier = this.FindByName<Picker>("supplierPickerModify");

        ModifyPartData modifyPartData = new ModifyPartData();
        modifyPartData.ModifyPart(IdPart, suppliers, reference, description, quantity, threshold, supplier);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

}

public class ModifyPartData
{
    public void ModifyPart(int id, List<Supplier> suppliers, Entry reference, Entry description, Entry quantity, Entry threshold, Picker supplier)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE dbNicebike.part SET Ref = @reference, Description = @description, Quantity = @quantity, Threshold = @threshold, Supplier = @supplier WHERE IdPart = @id";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@reference", reference.Text);
        command.Parameters.AddWithValue("@description", description.Text);
        command.Parameters.AddWithValue("@quantity", quantity.Text);
        command.Parameters.AddWithValue("@threshold", threshold.Text);
        command.Parameters.AddWithValue("@supplier", suppliers[supplier.SelectedIndex].idSupplier);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }
}