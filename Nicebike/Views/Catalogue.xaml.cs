namespace Nicebike.Views;

public partial class Catalogue : ContentPage
{
	public Catalogue()
	{
		InitializeComponent();
	}
    private async void NavigateToOrder(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderFilling());
    }
}
