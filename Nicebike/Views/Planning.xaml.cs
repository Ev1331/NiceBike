namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class Planning : ContentPage
{
    private PlanningManagement planningManagement;

    public Planning()
	{
		InitializeComponent();

        planningManagement = new PlanningManagement();
        List<Order> orderList = planningManagement.GetOrders();
        orderListPlanning.ItemsSource = orderList;
    }

    public async void ModifyDateProduction(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        var order = (Order)button.BindingContext;
        await Navigation.PushAsync(new ProductionDate(order));
        Navigation.RemovePage(this);

    }
}