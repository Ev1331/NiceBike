namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using System.Collections.ObjectModel;
using Nicebike.ViewModels;

public partial class StockManagement : ContentPage
{
    public ObservableCollection<Part> okParts = new ObservableCollection<Part>();
    public ObservableCollection<Part> lowParts = new ObservableCollection<Part>();
    public ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
    public PartsManagement stockManagement = new PartsManagement();
    public int IdPart;
    public StockManagement()
    {
        InitializeComponent();

        ObservableCollection<Part> observableParts = stockManagement.GetAllParts();

        foreach (Part part in observableParts){
            if(part.quantity < part.threshold)
            {
                lowParts.Add(part);
            }
            else
            {
                okParts.Add(part);
            }
        }
            partListView.ItemsSource = okParts;
            lowPartListView.ItemsSource = lowParts;
    }

    private async void NewPart(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PartDatasheet());
    }

    private async void OnModifyClickedPart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var part = (Part)button.BindingContext;
        var modifyPage = new ModifyPart(part);

        await Navigation.PushAsync(modifyPage);
    }

    private async void DeletePart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdPart = (int)button.CommandParameter;

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.DeletePart(IdPart);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

    private async void AddQuantityClick(object sender, EventArgs e)
    {
        Entry quantity = this.FindByName<Entry>("QuantityEntry");
        stockManagement.AddQuantity(IdPart, quantity);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

    private async void RemoveQuantityClick(object sender, EventArgs e)
    {
        Entry quantity = this.FindByName<Entry>("QuantityEntry");
        stockManagement.RemoveQuantity(IdPart, quantity);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);

    }

    private void partSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        //searchResults.ItemsSource = customersList.GetSearchResults(searchBar.Text);
        partSearchResults.ItemsSource = stockManagement.GetAllParts();
    }

    private void partSearchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        IdPart = ((Part)(partSearchResults.SelectedItem)).id;
    }

    private async void RestockAllClick(object sender, EventArgs e)
    {
        if (lowParts.Count != 0)
        {
        stockManagement.RestockAll(lowParts);
        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
        }
    }
}
