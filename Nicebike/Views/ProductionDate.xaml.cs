namespace Nicebike.Views;
using Nicebike.ViewModels;
using Nicebike.Models;

public partial class ProductionDate : ContentPage
{
    private PlanningManagement planningManagement = new PlanningManagement();
	private Order order;
	public ProductionDate(Order order)
	{
		InitializeComponent();
		this.order = order;
        BindingContext = order;
    }

	public async void ModifyClickedDate(object sender, EventArgs e)
	{
        Entry entry = this.FindByName<Entry>("DateEntry");
        string productionDate = entry.Text;

        planningManagement.ModifyProductionDate(order.IdOrder, productionDate);

		await Navigation.PushAsync(new Planning());
		Navigation.RemovePage(this);
    }
}