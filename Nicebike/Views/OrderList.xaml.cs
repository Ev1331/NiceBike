using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using Nicebike.Models;

namespace Nicebike.Views;

public partial class OrderList : ContentPage
{
    public OrderManagement orderManagement = new OrderManagement();
    public OrderList()
	{
		InitializeComponent();

        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        ObservableCollection<Order> orderList = orderManagement.GetAllOrders();

        // Assigner la liste des fournisseurs à la source de données du ListView
        orderListView.ItemsSource = orderList;
    }

    private void GoToNewOrder(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewOrderCustomerSelection());
    }

    private void ModifyOrder(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        Navigation.PushAsync(new OrderFilling(IdOrder));
    }
    private void DeleteOrder(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        orderManagement.DeleteOrder(IdOrder);
    }
}

public class OrderManagement
{
    public BikesManagement bikesManagement = new BikesManagement();
    public OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();
    MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
    public string sql;
public ObservableCollection<Order> GetAllOrders()
    {
        ObservableCollection<Order> orderList = new ObservableCollection<Order>();

        connection.Open();
        string sql = "SELECT * FROM dbNicebike.order";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Order order = new Order(
                        reader.GetInt32("IdOrder"),
                        reader.GetInt32("CustomerID"),
                        reader.GetString("Date"),
                        reader.GetString("DeliveryDate"),
                        reader.GetString("Status")
                    );
            orderList.Add(order);
        }
        connection.Close();
        return orderList;
    }

    public void DeleteOrder(int IdOrder)
    {
        List<Bike> orderBikes = orderDetailsManagement.GetOrderBikes(IdOrder);

        //Delete every bike from this order and every orderdetail associated
        foreach (Bike bike in orderBikes)
        {
            bikesManagement.DeleteBike(bike.id);
        }

        //Finally, deletes the order itself

        connection.Open();
        sql = "DELETE FROM dbNicebike.order WHERE IdOrder = @IdOrder";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public void CreateOrder(int IdCustomer)
    {   
        DateTime date = DateTime.Now;
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd");

        connection.Open();
        sql = "INSERT INTO dbNicebike.order (customerId, Date, DeliveryDate, Status) VALUES (@IdCustomer, @Date, @DeliveryDate, @Status)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdCustomer", IdCustomer);
        command.Parameters.AddWithValue("@Date", formattedDateTime);
        command.Parameters.AddWithValue("@DeliveryDate", "Undefined");
        command.Parameters.AddWithValue("@Status", "Waiting");
        command.ExecuteNonQuery();
        connection.Close();
    }
}