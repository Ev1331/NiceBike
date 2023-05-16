using Nicebike.Models;



using System.Data;
using MySql.Data.MySqlClient;


namespace Nicebike.Views


{
    public partial class MakeBike : ContentPage
    {
        public int TechnicianNumber { get; set; }
        public List<Bike> Bikes { get; set; }

        public MakeBike(int technicianNumber)
        {
            InitializeComponent();

            MakeBikeManagement makeBikeManagement = new MakeBikeManagement();

            Bikes = makeBikeManagement.BikesToBuild();

            TechnicianNumber = technicianNumber;

            
            BindingContext = this;

            UpdateLabelText();
        }

        private void UpdateLabelText()
        {
            technicianLabel.Text = $"Technicien {TechnicianNumber}";
        }
        public void OnProcessingClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var idBike = (int)button.CommandParameter;
            MakeBikeManagement makeBikeManagement = new MakeBikeManagement();

            makeBikeManagement.ProcessBike(idBike, TechnicianNumber);
        }
    }

    public class MakeBikeManagement
    {
        public List<Bike> BikesToBuild()
        {
            List<Bike> bikesToBuild = new List<Bike>();

            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM dbNicebike.bike";

            using MySqlCommand command = new MySqlCommand(sql, connection);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString("Status") == "Waiting")
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

                    bikesToBuild.Add(bike);
                }
            }

            return bikesToBuild;
        }

        public void ProcessBike(int IdBike, int IdTechnician)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "UPDATE dbNicebike.bike SET Technician = @technician, Status = 'InProgress' WHERE IdBike = @IdBike";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@technician", IdTechnician);
            command.Parameters.AddWithValue("@IdBike", IdBike);

            command.ExecuteNonQuery();
        }
        
    }
}
