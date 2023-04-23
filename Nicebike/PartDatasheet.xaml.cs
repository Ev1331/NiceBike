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
        EntryCell reference = this.FindByName<EntryCell>("referenceEntry");
        EntryCell description = this.FindByName<EntryCell>("descriptionEntry");
        EntryCell quantity = this.FindByName<EntryCell>("quantityEntry");
        EntryCell threshold = this.FindByName<EntryCell>("thresholdEntry");
        EntryCell supplier = this.FindByName<EntryCell>("supplierEntry");

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.SendPart(reference, description, quantity, threshold, supplier);
        
        Shell.Current.Navigation.RemovePage(this);
    }
}