namespace Nicebike;

public partial class TechnicianHome : ContentPage
{
    int technicianNumber;

    public TechnicianHome(int technicianNumber)
    {
        this.technicianNumber = technicianNumber;
        InitializeComponent();
        

        // Utilisez technicianNumber comme nécessaire dans votre page TechnicianHome
    }
    private async void NavigateToStock(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StockManagement());

	}

    private async void NavigateToMakeBike(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.MakeBike());
    }
    private async void NavigateToBuild(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Build());
    }
}

