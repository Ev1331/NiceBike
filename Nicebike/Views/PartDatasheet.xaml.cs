namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class PartDatasheet : ContentPage
{
    private List<Supplier> suppliers = new List<Supplier>();
    private SearchBarFilter searchBarFilter = new SearchBarFilter();
    
    private int IdSupplier;
    public PartDatasheet()
	{
		InitializeComponent();
        SupplierManagement supplierManagement = new SupplierManagement();
        suppliers = supplierManagement.GetAllSuppliers();
    }

    public async void SavePart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntry");
        Entry description = this.FindByName<Entry>("descriptionEntry");
        Entry quantity = this.FindByName<Entry>("quantityEntry");
        Entry threshold = this.FindByName<Entry>("thresholdEntry");

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.SendPart(suppliers, reference, description, quantity, threshold, IdSupplier);

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