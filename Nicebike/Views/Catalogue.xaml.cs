namespace Nicebike.Views;

using Nicebike.Models;
using Nicebike.ViewModels;

public partial class Catalogue : ContentPage
{
    private BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
    private List<BikeModel> bikeModels = new List<BikeModel>();
    public Catalogue()
	{
		InitializeComponent();
        bikeModels = bikeModelsManagement.GetAllBikeModels();
	}
    private async void NavigateToOrder(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderList());
    }
    private async void OnCityImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CityInfo(bikeModels.Find(obj => obj.description == "City").price));
    }
    private async void OnExplorerImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExplorerInfo(bikeModels.Find(obj => obj.description == "Explorer").price));
    }
    private async void OnAdventureImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdventureInfo(bikeModels.Find(obj => obj.description == "Adventure").price));
    }
}
