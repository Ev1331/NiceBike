namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.Views;
using Nicebike.ViewModels;
using System.Collections.ObjectModel;

public partial class NewOrderCustomerSelection : ContentPage
{
    public int IdOrder;
    CustomersManagement customersManagement = new CustomersManagement();
    public OrderManagement orderManagement = new OrderManagement();
    public NewOrderCustomerSelection()
	{
		InitializeComponent();
	}

    private void CustomerSearchClick(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        //searchResults.ItemsSource = customersList.GetSearchResults(searchBar.Text);
        searchResults.ItemsSource = customersManagement.GetAllCustomers();
    }

    private void GoToCustomersManagement(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClientsManagement());
    }

    private async void InitialiseOrder(object sender, EventArgs e)
    {
        int IdCustomer = ((Customer)(searchResults.SelectedItem)).idCustomer;
        orderManagement.CreateOrder(IdCustomer);

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT IdOrder FROM dbNicebike.order ORDER BY IdOrder DESC LIMIT 1";
        MySqlCommand command= new MySqlCommand(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            IdOrder = reader.GetInt32("IdOrder");
        }

        await Navigation.PushAsync(new OrderFilling(IdOrder));
        Navigation.RemovePage(this);
    }
}