namespace Nicebike.Views;

public partial class CityInfo : ContentPage
{
	public CityInfo(int price)
	{
		InitializeComponent();
        cityPriceLabel.Text = price.ToString();
    }
}