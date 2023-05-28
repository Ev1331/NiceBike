namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Reflection.Metadata;

public partial class ModifyPart : ContentPage
{
    List<String> suppliersnames = new List<String>();
    List<Supplier> suppliers = new List<Supplier>();
    private SearchBarFilter searchBarFilter = new SearchBarFilter();

    Part part;
    private int IdSupplier;
    int IdPart;
    int i = 0;
    public ModifyPart(Part part)
    {
        InitializeComponent();
        this.part = part;
        IdPart = part.id;

        SupplierManagement supplierManagement = new SupplierManagement();
        suppliers = supplierManagement.GetAllSuppliers();

        BindingContext = part;
    }

    public async void modifyClickedPart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntryModify");
        Entry description = this.FindByName<Entry>("descriptionEntryModify");
        Entry quantity = this.FindByName<Entry>("quantityEntryModify");
        Entry threshold = this.FindByName<Entry>("thresholdEntryModify");

        ModifyDataPart modifyDataPart = new ModifyDataPart();
        modifyDataPart.ModifyPart(IdPart, suppliers, reference, description, quantity, threshold, IdSupplier);

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
