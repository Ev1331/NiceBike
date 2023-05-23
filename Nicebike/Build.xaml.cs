

using Nicebike.Models;
using System.Data;
using MySql.Data.MySqlClient;
namespace Nicebike;



public partial class Build : ContentPage
{
    public int BuilderNumber { get; set; }
    public List<Bike> BikesForBuilder { get; set; }

    public Build(int builder)
    {
        InitializeComponent();

        BuilderNumber = builder;
        BuildManagement buildManagement = new BuildManagement();
        BikesForBuilder = buildManagement.BikesForBuilder(BuilderNumber);

        BindingContext = this;

        UpdateLabelText();
    }

    private void UpdateLabelText()
    {
        technicianLabel.Text = $"Technicien {BuilderNumber}";
    }
    private void OnFinishedClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idBike = (int)button.CommandParameter;
        BuildManagement buildManagement = new BuildManagement();

        buildManagement.FinishBike(idBike, BuilderNumber);
    }


}

public class BuildManagement
{
    public List<Bike> BikesForBuilder(int id)
    {
        BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
        List<BikeModel> bikeModels = new List<BikeModel>();
        bikeModels = bikeModelsManagement.GetAllBikeModels();
        int BikeModelId;

        List<Bike> bikesForBuilder = new List<Bike>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.bike";

        using MySqlCommand command = new MySqlCommand(sql, connection);

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            if (reader.GetString("Status") == "InProgress" && reader.GetInt32("Technician") == id)
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

                bikesForBuilder.Add(bike);
            }
        }

        return bikesForBuilder;


    }
    public void FinishBike(int IdBike, int IdTechnician)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE dbNicebike.bike SET Technician = @technician, Status = 'Done' WHERE IdBike = @IdBike";
        MySqlCommand command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@technician", IdTechnician);
        command.Parameters.AddWithValue("@IdBike", IdBike);

        command.ExecuteNonQuery();
    }
}
