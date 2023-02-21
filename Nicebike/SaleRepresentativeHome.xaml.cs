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
}
