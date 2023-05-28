namespace Nicebike;

public partial class ExplorerInfo : ContentPage
{
	public ExplorerInfo(int price)
	{
		InitializeComponent();
        explorerPriceLabel.Text = price.ToString();
    }
}