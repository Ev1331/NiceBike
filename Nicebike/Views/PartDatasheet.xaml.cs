namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class PartDatasheet : ContentPage
{
    List<String> suppliersnames = new List<String>();
    List<Supplier> suppliers = new List<Supplier>();

    int i = 0;
    public PartDatasheet()
	{
		InitializeComponent();
        // Cr�er une instance de la classe SupplierManagement
        SupplierManagement supplierManagement = new SupplierManagement();

        // R�cup�rer la liste des fournisseurs � partir de la base de donn�es
        suppliers = supplierManagement.GetAllSuppliers();

        foreach (Supplier supplier in suppliers)
        {
            suppliersnames.Add(suppliers[i].name);
            i = i + 1;
        }

        // Assigner la liste des fournisseurs � la source de donn�es du ListView
        supplierPicker.ItemsSource = suppliersnames;
    }

    public async void SavePart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntry");
        Entry description = this.FindByName<Entry>("descriptionEntry");
        Entry quantity = this.FindByName<Entry>("quantityEntry");
        Entry threshold = this.FindByName<Entry>("thresholdEntry");
        Picker supplier = this.FindByName<Picker>("supplierPicker");

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.SendPart(suppliers, reference, description, quantity, threshold, supplier);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }
}