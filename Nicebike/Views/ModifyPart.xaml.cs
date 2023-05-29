namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Reflection.Metadata;

public partial class ModifyPart : ContentPage
{
    private List<Supplier> suppliers = new List<Supplier>();
    private SearchBarFilter searchBarFilter = new SearchBarFilter();
    private PartsManagement partsManagement = new PartsManagement();

    private int IdSupplier;
    int IdPart;
    public ModifyPart(Part part)
    {
        SupplierManagement supplierManagement = new SupplierManagement();

        InitializeComponent();

        IdPart = part.id;
        suppliers = supplierManagement.GetAllSuppliers();
        BindingContext = part;
    }

    public async void modifyClickedPart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntryModify");
        Entry description = this.FindByName<Entry>("descriptionEntryModify");
        Entry quantity = this.FindByName<Entry>("quantityEntryModify");
        Entry threshold = this.FindByName<Entry>("thresholdEntryModify");

        partsManagement.ModifyPart(IdPart, suppliers, reference, description, quantity, threshold, IdSupplier);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

    private void supplierSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        supplierSearchResults.ItemsSource = searchBarFilter.GetFilteredSuppliers(((SearchBar)sender).Text);
    }

    private void supplierSearchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        IdSupplier = ((Supplier)(supplierSearchResults.SelectedItem)).idSupplier;
    }

    private void ManageSuppliers(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SuppliersManagement());
    }
}