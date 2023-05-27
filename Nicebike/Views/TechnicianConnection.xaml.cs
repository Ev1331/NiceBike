namespace Nicebike.Views;

public partial class TechnicianConnection : ContentPage
{
	public TechnicianConnection()
	{
		InitializeComponent();
	}

    private async void NavigateToTechnician(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idTechnician = Convert.ToInt32(button.CommandParameter);
        //Navigation.PushAsync(new TechnicianHome(idTechnician));
        var homeTechnician = new TechnicianHome(idTechnician);


       Navigation.PushAsync(homeTechnician);
    }
}


