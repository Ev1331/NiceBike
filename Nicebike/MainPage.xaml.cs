namespace Nicebike;

public partial class MainPage : ContentPage
{
	int count = 0;

    private async void NavigateTo(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderFilling());
    }

    public MainPage()
	{
		InitializeComponent();
	}
}



