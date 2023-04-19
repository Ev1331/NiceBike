
namespace Nicebike;

public partial class MainPage : ContentPage
{
    private async void NavigateToTechnician(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TechnicianConnection());
    }

   
    private async void NavigateToSaleRepresentative(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SaleRepresentativeHome());
    }
    private async void NavigateToOwner(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OwnerHome());
    }
    private async void NavigateToProductionManager(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProductionManagerHome());
    }
    public MainPage()
	{
		InitializeComponent();
	}

    
}



