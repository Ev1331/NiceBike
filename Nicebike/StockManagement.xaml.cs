namespace Nicebike;

public partial class StockManagement : ContentPage
{
	public StockManagement()
	{
		InitializeComponent();
	}
    private async void NewPart(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PartDatasheet());
    }
}