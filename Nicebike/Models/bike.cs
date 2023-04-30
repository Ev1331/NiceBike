using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Nicebike.Models
{
    public class Bike
    {
        public int id { get; set; }
        public string color { get; set; }
        public string type { get; set; }
        public string size { get; set; }
        public string reference { get; set; }
        public int technician { get; set; }
        public int bikeModel { get; set; }
        public string status { get; set; }

        public Bike(int id, string color, string type, string size, string reference, int technician, int bikeModel, string status)
        {
            this.id = id;
            this.color = color;
            this.type = type;
            this.size = size;
            this.reference = reference;
            this.technician = technician;
            this.bikeModel = bikeModel;
            this.status = status;
        }
    }

    public class BikeDB
    {
        public ObservableCollection<Bike> GetAllParts()
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
    }
}