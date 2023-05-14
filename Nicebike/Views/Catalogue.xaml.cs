namespace Nicebike.Views;

public partial class Catalogue : ContentPage
{
	public Catalogue()
	{
		InitializeComponent();
	}
    private async void NavigateToOrder(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderList());
    }
    private async void OnImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CityInfo());
    }
    private async void OnExplorerImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExplorerInfo());
    }
    private async void OnAdventureImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdventureInfo());
    }
}
