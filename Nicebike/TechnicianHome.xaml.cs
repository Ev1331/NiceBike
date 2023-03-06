namespace Nicebike;

public partial class TechnicianHome : ContentPage
{
	public TechnicianHome()
	{
		InitializeComponent();

    }
    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());

	}

    private async void NavigateToMakeBike(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.MakeBike());
    }
}
