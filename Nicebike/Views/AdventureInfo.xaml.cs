namespace Nicebike;

public partial class AdventureInfo : ContentPage
{
    public AdventureInfo(int price)
	{
        InitializeComponent();
        adventurePriceLabel.Text = price.ToString();
    }
}