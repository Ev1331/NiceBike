using Nicebike.Views;

namespace Nicebike;

public partial class OwnerHome : ContentPage
{
	public OwnerHome()
	{
		InitializeComponent();
    }
    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());
    }
    private async void NavigateToEmployeeMgmt(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EmployeeMgmt());
    }
    
}
