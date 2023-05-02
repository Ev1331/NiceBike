namespace Nicebike;
using Nicebike.Views;

public partial class SaleRepresentativeHome : ContentPage
{
	public SaleRepresentativeHome()
	{
		InitializeComponent();
	}

    private async void NavigateToOrder(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderList());
    }
    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());
    }
    private async void NavigateToCatalogue(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Catalogue());
    }

    private async void NavigateToCustomer(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClientsManagement());
    }
}
