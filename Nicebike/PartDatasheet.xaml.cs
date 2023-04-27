namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;

public partial class PartDatasheet : ContentPage
{
	public PartDatasheet()
	{
		InitializeComponent();
	}

    public void SavePart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntry");
        Entry description = this.FindByName<Entry>("descriptionEntry");
        Entry quantity = this.FindByName<Entry>("quantityEntry");
        Entry threshold = this.FindByName<Entry>("thresholdEntry");
        Entry supplier = this.FindByName<Entry>("supplierEntry");

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.SendPart(reference, description, quantity, threshold, supplier);
        
        Shell.Current.Navigation.RemovePage(this);
    }
}