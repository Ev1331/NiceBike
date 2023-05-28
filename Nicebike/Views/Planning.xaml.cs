namespace Nicebike.Views;
using Nicebike.Models;
using MySql.Data.MySqlClient;
using System.Globalization;
using Nicebike.ViewModels;

public partial class Planning : ContentPage
{
    public PlanningManagement orderPlanning = new PlanningManagement();

    public Planning()
	{
		InitializeComponent();

        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        List<Order> orderList = orderPlanning.GetOrders();

        // Assigner la liste des fournisseurs � la source de donn�es du ListView
        orderListPlanning.ItemsSource = orderList;
    }

    public void ModifyDate(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        Entry productionDate = this.FindByName<Entry>("ProductionDate");

        orderPlanning.ModifyProductionDate(IdOrder, productionDate.Text);

        List<Order> orderList = orderPlanning.GetOrders();

        // Assigner la liste des fournisseurs � la source de donn�es du ListView
        orderListPlanning.ItemsSource = orderList;

    }
}
