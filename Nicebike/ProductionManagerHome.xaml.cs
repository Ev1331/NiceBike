namespace Nicebike;

public partial class ProductionManagerHome : ContentPage
{
	public ProductionManagerHome()
	{
		InitializeComponent();
    }
    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());
    }
}
