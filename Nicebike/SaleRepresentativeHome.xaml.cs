namespace Nicebike;

public partial class SaleRepresentativeHome : ContentPage
{
	public SaleRepresentativeHome()
	{
		InitializeComponent();
	}

    private async void NavigateToOrder(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderFilling());
    }
    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());
    }
    private async void NavigateToCatalogue(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Catalogue());
    }
}
