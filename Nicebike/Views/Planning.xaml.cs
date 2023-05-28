namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class Planning : ContentPage
{
    public PlanningManagement planningManagement;

    public Planning()
	{
		InitializeComponent();

        planningManagement = new PlanningManagement();

        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        List<Order> orderList = planningManagement.GetOrders();

        // Assigner la liste des fournisseurs � la source de donn�es du ListView
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
