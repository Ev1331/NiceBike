namespace Nicebike.Views;

public partial class MakeBike : ContentPage
{
    public MakeBike()
    {
        InitializeComponent();
    }

    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());
    }

}
