namespace Nicebike;

using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.Views;
using System.Collections.ObjectModel;
using System.Globalization;

public partial class OrderFilling : ContentPage
{
    //ListView filling
    string[] colorList = { "Red", "Blue", "Grey"};
    List<String> modelList = new List<String>();
    string[] sizeList = { "26\"", "28\""};

    List<BikeModel> bikeModels = new List<BikeModel>();
    List<Order> orders = new List<Order>();
    int i=0;
    public int IdOrder;
    public Customer orderCustomer;

    CustomersManagement customersManagement = new CustomersManagement();
    BikesManagement bikesManagement = new BikesManagement();
    BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
    OrderManagement orderManagement = new OrderManagement();
    OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();

    public OrderFilling(int Id)
	{
        List<Customer> customers = new List<Customer>();
        int IdCustomer;
        
		InitializeComponent();
        IdOrder = Id; // IdOrder accessible for the SaveBike function, do not remove (!)

        bikeModels = bikeModelsManagement.GetAllBikeModels();
        foreach (BikeModel bikeModel in bikeModels)
        {
            modelList.Add(bikeModels[i].description);
            i++;
        }

        orderDetailsListView.ItemsSource = orderDetailsManagement.GetOrderBikes(IdOrder); //List of the bikes from this order
        colorPicker.ItemsSource = colorList;
        modelPicker.ItemsSource = modelList;
        sizePicker.ItemsSource = sizeList;

        orders = orderManagement.GetAllOrders();
        IdCustomer = (orders.Find(obj => obj.IdOrder == Id)).CustomerId;
        customers = customersManagement.GetAllCustomers();
        orderCustomer = customers.Find(obj => obj.idCustomer == IdCustomer);
        BindingContext = orderCustomer;
    }

    private void RemoveBike(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdBike = (int)button.CommandParameter;
        bikesManagement.DeleteBike(IdBike);
    }

    public async void SaveBike(object sender, EventArgs e)
    {

        Picker color = this.FindByName<Picker>("colorPicker");
        Picker size = this.FindByName<Picker>("sizePicker");
        Picker model = this.FindByName<Picker>("modelPicker");
        Entry quantity = this.FindByName<Entry>("quantityEntry");

        int number;
        bool success = int.TryParse(quantity.Text, out number);

        if (success)
        {
            for (int i = 0; i < number; i++)
            {
                bikesManagement.SendBike(colorList, sizeList, bikeModels, color, "...TYPE...", size, "...REF...", model, "Waiting", IdOrder);
            }
        }


        await Navigation.PushAsync(new OrderFilling(IdOrder));
        Navigation.RemovePage(this);

    }

    private void GoToCustomersManagementClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClientsManagement());
    }

    private async void Confirm(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OrderList());
        Navigation.RemovePage(this);
    }
}

public class BikesManagement
{
    int IdBike1;
    int IdOrder1;
    string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
    int BikecountPlus = 0;
    int BikecountMinus = 6;
    public List<Bike> GetAllBikes()
    {
        BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
        List<BikeModel> bikeModels = new List<BikeModel>();
        bikeModels = bikeModelsManagement.GetAllBikeModels();
        int BikeModelId;
        List<Bike> bikes = new List<Bike>();

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.bike";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            BikeModelId = reader.GetInt32("BikeModel");
            Bike bike = new Bike(
                reader.GetInt32("IdBike"),
                reader.GetString("Colour"),
                reader.GetString("Type"),
                reader.GetString("Size"),
                reader.GetString("Ref"),
                reader.GetInt32("Technician"),
                BikeModelId,
                reader.GetString("Status"),
                bikeModels.Find(obj => obj.id == BikeModelId).description
            );
            bikes.Add(bike);
        }
        return bikes;
    }
    public void DeleteBike(int IdBike)
    {
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM dbNicebike.orderdetails WHERE Bike = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", IdBike);

        command.ExecuteNonQuery();

        sql = "DELETE FROM dbNicebike.bike WHERE IdBike = @id";
        using MySqlCommand command2 = new MySqlCommand(sql, connection);
        command2.Parameters.AddWithValue("@id", IdBike);

        command2.ExecuteNonQuery();

        

        

    }
    public void SendBike(string[] colorList, string[] sizeList, List<BikeModel> bikeModels, Picker color, string type, Picker size, string reference, Picker bikeModel, string status, int IdOrder)
    {
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO dbNicebike.bike (Colour, Type, Size, Ref, Technician, BikeModel, Status) VALUES (@Colour, @Type, @Size, @Ref, @Technician, @BikeModel, @Status)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Colour", colorList[color.SelectedIndex]);
        command.Parameters.AddWithValue("@Type", type);
        command.Parameters.AddWithValue("@Size", sizeList[size.SelectedIndex]);
        command.Parameters.AddWithValue("@Ref", reference);
        command.Parameters.AddWithValue("@Technician", 0); //Bike ordered: no technician assigned yet
        command.Parameters.AddWithValue("@BikeModel", bikeModels[bikeModel.SelectedIndex].id);
        command.Parameters.AddWithValue("@Status", status);

        command.ExecuteNonQuery();

        sql = "SELECT IdBike FROM dbNicebike.bike ORDER BY IdBike DESC LIMIT 1";
        MySqlCommand command2 = new MySqlCommand(sql, connection);
        MySqlDataReader reader = command2.ExecuteReader();

        while (reader.Read())
        {
            IdBike1 = reader.GetInt32("IdBike");  
        }

        using MySqlConnection connection2 = new MySqlConnection(connectionString);
        connection2.Open();
        sql = "INSERT INTO dbNicebike.orderdetails (IdOrder, Bike) VALUES (@IdOrder, @IdBike)";
        MySqlCommand command3 = new MySqlCommand(sql, connection2);
        command3.Parameters.AddWithValue("@IdOrder", IdOrder);
        command3.Parameters.AddWithValue("@IdBike", IdBike1);
        command3.ExecuteNonQuery();


        BikecountPlus++;
        if (BikecountPlus == 6)
        {
            BikecountPlus = 0;
            string newDeliveryDate = "";
            using MySqlConnection connection3 = new MySqlConnection(connectionString);
            connection3.Open();
            sql = "SELECT * FROM dbNicebike.order WHERE IdOrder = @IdOrder";
            using MySqlCommand command4 = new MySqlCommand(sql, connection3);
            command4.Parameters.AddWithValue("@IdOrder", IdOrder);
            using MySqlDataReader reader1 = command4.ExecuteReader();

            while (reader1.Read())
            {
                string DeleveryDate = reader1.GetString("DeliveryDate");
                DateTime date = DateTime.ParseExact(DeleveryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime newDate = date.AddDays(1);
                newDeliveryDate = newDate.ToString("yyyy-MM-dd");
            }

            using MySqlConnection connection4 = new MySqlConnection(connectionString);
            connection4.Open();
            sql = "UPDATE dbNicebike.order SET DeliveryDate = @newDeliveryDate WHERE IdOrder = @IdOrder";
            MySqlCommand command5 = new MySqlCommand(sql, connection4);
            command5.Parameters.AddWithValue("@newDeliveryDate", newDeliveryDate);
            command5.Parameters.AddWithValue("@IdOrder", IdOrder);

            command5.ExecuteNonQuery();

        }
    }
}

public class BikeModelsManagement
{
    public List<BikeModel> GetAllBikeModels()
    {
        List<BikeModel> bikeModels = new List<BikeModel>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.bikemodel";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            BikeModel bikeModel = new BikeModel(
                reader.GetInt32("IdBikeModel"),
                reader.GetInt32("Price"),
                reader.GetString("Description")
            );
            bikeModels.Add(bikeModel);
        }
        return bikeModels;
    }
}
public class OrderDetailsManagement
{
    public int id;
    List<int> bikesIdList = new List<int>();
    public List<Bike> orderBikes = new List<Bike>();

    MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
    string sql;
public List<Bike> GetOrderBikes(int IdOrder)
    {
        List<Bike> bikes = new List<Bike>();

        BikesManagement bikesManagement = new BikesManagement();
        List<Bike> observableBikes = bikesManagement.GetAllBikes();

        connection.Open();
        sql = "SELECT * FROM dbNicebike.orderdetails WHERE IdOrder = @IdOrder";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            id = reader.GetInt32("Bike");
            bikesIdList.Add(id); 
            orderBikes.Add(observableBikes.Find(obj => obj.id == id)); //Monte une liste avec les v�los correspondants
        }
        connection.Close();

    return orderBikes;
    }

public int GetAssociatedOrderId(int IdBike)
    {
        connection.Open();
        sql = "SELECT * FROM dbNicebike.orderdetails WHERE Bike = @IdBike";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdBike", IdBike);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            id = reader.GetInt32("IdOrder");
        }

        connection.Close();
     return id;
    }
}