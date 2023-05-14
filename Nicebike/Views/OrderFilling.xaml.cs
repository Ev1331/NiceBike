namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.Views;
using System.Collections.ObjectModel;

public partial class OrderFilling : ContentPage
{
    string[] colorList = { "Red", "Blue", "Grey"};
    string[] sizeList = { "26\"", "28\""};
    List<String> modelList = new List<String>();
    List<BikeModel> bikeModels = new List<BikeModel>();
    ObservableCollection<Order> orderDetailsList = new ObservableCollection<Order>();
    int i=0;


    List<int> bikesIdList = new List<int>();

    public OrderFilling(int IdOrder)
	{
		InitializeComponent();

        /*
        OrderManagement orderManagement = new OrderManagement();
        orderDetailsList = orderManagement.GetAllOrders();

        BikesManagement bikesManagement = new BikesManagement();


        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        ObservableCollection<Bike> observableBikes = bikesManagement.GetAllBikes();


        */

        // Récupérer la liste des modèls de vélo à partir de la base de données
        BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
        bikeModels = bikeModelsManagement.GetAllBikeModels();

        foreach (BikeModel bikeModel in bikeModels)
        {
            modelList.Add(bikeModels[i].description);
            i++;
        }

        OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();
        bikesIdList = orderDetailsManagement.GetOrderDetails(IdOrder);

        orderDetailsListView.ItemsSource = bikesIdList; //orderDetailsList


        colorPicker.ItemsSource = colorList;
        modelPicker.ItemsSource = modelList;
        sizePicker.ItemsSource = sizeList;
    }

    private void RemoveBike(object sender, EventArgs e)
    {

    }
    private void AddBike(object sender, EventArgs e)
    {

    }
    public void SaveBike(object sender, EventArgs e)
    {
        Picker color = this.FindByName<Picker>("colorPicker");
        Picker size = this.FindByName<Picker>("sizePicker");
        Picker model = this.FindByName<Picker>("modelPicker");

        BikesManagement bikesManagement = new BikesManagement();
        bikesManagement.SendBike(colorList, sizeList, bikeModels, color, "...TYPE...", size,  "...REF...", model, "Waiting");
    }
}

public class BikesManagement
{
    public ObservableCollection<Bike> GetAllBikes()
    {
        ObservableCollection<Bike> bikes = new ObservableCollection<Bike>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.bike";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Bike bike = new Bike(
                reader.GetInt32("IdBike"),
                reader.GetString("Colour"),
                reader.GetString("Type"),
                reader.GetString("Size"),
                reader.GetString("Ref"),
                reader.GetInt32("Technician"),
                reader.GetInt32("BikeModel"),
                reader.GetString("Status")
            );
            bikes.Add(bike);
        }
        return bikes;
    }

    public void DeleteBike(int IdBike)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM dbNicebike.bike WHERE idBike = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", IdBike);

        command.ExecuteNonQuery();

    }
    public void SendBike(string[] colorList, string[] sizeList, List<BikeModel> bikeModels, Picker color, string type, Picker size, string reference, Picker bikeModel, string status)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

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
    int id;
    List<int> bikesIdList = new List<int>();
    List<string> orderIdList = new List<string>();
    public List<int> GetOrderDetails(int IdOrder)

    {
        List<Bike> bikes = new List<Bike>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.orderdetails WHERE IdOrder = @IdOrder";
        //string sql = "SELECT IdOrderDetails FROM dbNicebike.orderdetails where Order=@IdOrder";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            id = reader.GetInt32("Bike");
            bikesIdList.Add(id);
        }
        return bikesIdList;

        /*
        sql = "SELECT * FROM dbNicebike.bike";
        using MySqlCommand command2 = new MySqlCommand(sql, connection);
        using MySqlDataReader reader2 = command2.ExecuteReader();

        command2.ExecuteReader();

        while (reader2.Read())
        {
            Bike bike = new Bike(
                reader2.GetInt32("IdBike"),
                reader2.GetString("Colour"),
                reader2.GetString("Type"),
                reader2.GetString("Size"),
                reader2.GetString("Ref"),
                reader2.GetInt32("Technician"),
                reader2.GetInt32("BikeModel"),
                reader2.GetString("Status")
            );
            bikes.Add(bike);
        }
        */

        //return bikeModels;
    }
}