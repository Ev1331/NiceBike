namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using System.Collections.ObjectModel;

public partial class OrderFilling : ContentPage
{
    string[] colorList = { "Red", "Blue", "Grey"};
    string[] sizeList = { "Small", "Standard", "Large" };
    List<String> modelList = new List<String>();
    List<BikeModel> bikeModels = new List<BikeModel>();
    int i=0;

    public OrderFilling()
	{
		InitializeComponent();

        // Créer une instance de la classe SupplierManagement
        BikesManagement bikesManagement = new BikesManagement();
        BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();

        // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        ObservableCollection<Bike> observableBikes = bikesManagement.GetAllBikes();

        bikesListView.ItemsSource = observableBikes;

        // Récupérer la liste des fournisseurs à partir de la base de données
        bikeModels = bikeModelsManagement.GetAllBikeModels();

        foreach (BikeModel bikeModel in bikeModels)
        {
            modelList.Add(bikeModels[i].description);
            i++;
        }

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