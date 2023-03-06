namespace Nicebike;

public partial class TechnicianConnection : ContentPage
{
	public TechnicianConnection()
	{
		InitializeComponent();
	}

    private async void NavigateToTechnician(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TechnicianHome());
    }
}
