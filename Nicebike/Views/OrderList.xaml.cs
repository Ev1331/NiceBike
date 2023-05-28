using System.Collections.ObjectModel;
using System.Globalization;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

namespace Nicebike.Views;

public partial class OrderList : ContentPage
{
    public OrderManagement orderManagement = new OrderManagement();
    public OrderList()
    {
        InitializeComponent();

        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        List<Order> orderList = orderManagement.GetAllOrders();

        // Assigner la liste des fournisseurs � la source de donn�es du ListView
        orderListView.ItemsSource = orderList;
    }

    private async void GoToNewOrder(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewOrderCustomerSelection());
        Navigation.RemovePage(this);
    }

    private async void ModifyOrder(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        await Navigation.PushAsync(new OrderFilling(IdOrder));
        Navigation.RemovePage(this);
    }
    private async void DeleteOrder(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        orderManagement.DeleteOrder(IdOrder);

        List<Order> orderList = orderManagement.GetAllOrders();
        orderListView.ItemsSource = orderList;
    }
}
