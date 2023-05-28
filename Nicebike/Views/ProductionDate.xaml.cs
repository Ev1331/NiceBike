namespace Nicebike.Views;
using Nicebike.ViewModels;
using Nicebike.Models;

public partial class ProductionDate : ContentPage
{
	PlanningManagement planningManagement = new PlanningManagement();
	Order order;
	public ProductionDate(Order order)
	{
		InitializeComponent();
		this.order = order;

        BindingContext = order;
    }

	public void ModifyClickedDate(object sender, EventArgs e)
	{
        Entry entry = this.FindByName<Entry>("DateEntry");
        string productionDate = entry.Text;

        planningManagement.ModifyProductionDate(order.IdOrder, productionDate);

		Navigation.PushAsync(new Planning());
		Navigation.RemovePage(this);
    }
}
