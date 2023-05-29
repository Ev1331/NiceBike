namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.Views;
using Nicebike.ViewModels;
using System.Collections.ObjectModel;

public partial class NewOrderCustomerSelection : ContentPage
{
    private OrderManagement orderManagement = new OrderManagement();
    private SearchBarFilter searchBarFilter = new SearchBarFilter();
    private int IdOrder;
    public NewOrderCustomerSelection()
	{
		InitializeComponent();
	}

    private void CustomerSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        customerSearchResults.ItemsSource = searchBarFilter.GetFilteredCustomers(((SearchBar)sender).Text);
    }

    private void GoToCustomersManagement(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClientsManagement());
    }

    private async void InitialiseOrder(object sender, EventArgs e)
    {
        int IdCustomer = ((Customer)(customerSearchResults.SelectedItem)).idCustomer;
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