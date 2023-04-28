namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;

public partial class PartDatasheet : ContentPage
{
    List<String> suppliersnames = new List<String>();
    List<Supplier> suppliers = new List<Supplier>();

    int i = 0;
    public PartDatasheet()
	{
		InitializeComponent();
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
        supplierPicker.ItemsSource = suppliersnames;
    }

    public void SavePart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntry");
        Entry description = this.FindByName<Entry>("descriptionEntry");
        Entry quantity = this.FindByName<Entry>("quantityEntry");
        Entry threshold = this.FindByName<Entry>("thresholdEntry");
        Picker supplier = this.FindByName<Picker>("supplierPicker");

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.SendPart(suppliers, reference, description, quantity, threshold, supplier);
        
        Shell.Current.Navigation.RemovePage(this);
    }
}