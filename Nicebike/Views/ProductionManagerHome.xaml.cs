namespace Nicebike.Views;

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
    private async void NavigateToSuppliers(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SuppliersManagement());
    }

    private async void NavigateToPlanning(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Planning());
    }
}
