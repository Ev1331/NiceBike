namespace Nicebike.Views;

using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class SuppliersManagement : ContentPage
{
    public SuppliersManagement()
    {
        InitializeComponent();

        // Créer une instance de la classe SupplierManagement
        SupplierManagement supplierManagement = new SupplierManagement();

        // Récupérer la liste des fournisseurs à partir de la base de données
        List<Supplier> suppliers = supplierManagement.GetAllSuppliers();

        // Assigner la liste des fournisseurs à la source de données du ListView
        supplierListView.ItemsSource = suppliers;
    }
    public async void OnConfirmClicked(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameEntry");
        Entry mail = this.FindByName<Entry>("mailEntry");
        Entry phone = this.FindByName<Entry>("phoneEntry");
        Entry street = this.FindByName<Entry>("streetEntry");
        Entry number = this.FindByName<Entry>("numberEntry");
        Entry town = this.FindByName<Entry>("townEntry");

        SupplierManagement supplierManagement = new SupplierManagement();

        supplierManagement.SendSupplier(name, mail, phone, street, town, number);

        await Navigation.PushAsync(new SuppliersManagement());
        Navigation.RemovePage(this);
    }
    public async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idSupplier = (int)button.CommandParameter;

        SupplierManagement supplierManagement = new SupplierManagement();
        supplierManagement.DeleteSupplier(idSupplier);

        await Navigation.PushAsync(new SuppliersManagement());
        Navigation.RemovePage(this);
    }

    private async void OnModifyClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var supplier = (Supplier)button.BindingContext;
        var modifyPage = new ModifySupplier(supplier);

        await Navigation.PushAsync(modifyPage);
        Navigation.RemovePage(this);
    }
}