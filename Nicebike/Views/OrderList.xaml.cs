using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using Nicebike.Models;

namespace Nicebike.Views;

public partial class OrderList : ContentPage
{
	public OrderList()
	{
		InitializeComponent();
        // Créer une instance de la classe SupplierManagement
        OrderManagement orderManagement = new OrderManagement();

        // Récupérer la liste des fournisseurs à partir de la base de données
        //List<Part> parts = stockManagement.GetAllParts();

        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        ObservableCollection<Order> orderList = orderManagement.GetAllOrders();

        // Assigner la liste des fournisseurs à la source de données du ListView
        orderListView.ItemsSource = orderList;
    }

    private void DeleteOrder(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        OrderManagement orderManagement = new OrderManagement();
        orderManagement.DeleteOrder(IdOrder);
    }

    private void GoToNewOrder(object sender, EventArgs e)
    {

    }

    private void ModifyOrder(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdOrder = (int)button.CommandParameter;

        Navigation.PushAsync(new OrderFilling(IdOrder));
    }
}

public class OrderManagement
{
    public ObservableCollection<Order> GetAllOrders()
    {
        ObservableCollection<Order> orderList = new ObservableCollection<Order>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
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
        return orderList;
    }

    /*
    public void SendOrder(List<Supplier> suppliers, Entry reference, Entry description, Entry quantity, Entry threshold, Picker supplier)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO dbNicebike.part (Ref, Description, Quantity, Threshold, Supplier) VALUES (@reference, @description, @quantity, @threshold, @supplier)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@reference", reference.Text);
        command.Parameters.AddWithValue("@description", description.Text);
        command.Parameters.AddWithValue("@quantity", quantity.Text);
        command.Parameters.AddWithValue("@threshold", threshold.Text);
        command.Parameters.AddWithValue("@supplier", suppliers[supplier.SelectedIndex].idSupplier);

        command.ExecuteNonQuery();
    }
    */

    public void DeleteOrder(int IdOrder)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM dbNicebike.order WHERE id = @IdOrder";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);

        command.ExecuteNonQuery();
    }
}
