using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class MakeBikeManagement
	{
        public List<Bike> BikesToBuild()
        {
            List<Bike> bikesToBuild = new List<Bike>();
            BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
            List<BikeModel> bikeModels = new List<BikeModel>();
            bikeModels = bikeModelsManagement.GetAllBikeModels();

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
                    int BikeModelId = reader.GetInt32("BikeModel");
                    string size = reader.GetString("Size");
                    Bike bike = new Bike(
                        reader.GetInt32("IdBike"),
                        reader.GetString("Colour"),
                        reader.GetString("Type"),
                        size,
                        reader.GetString("Ref"),
                        reader.GetInt32("Technician"),
                        BikeModelId,
                        reader.GetString("Status"),
                        bikeModels.Find(obj => obj.id == BikeModelId).description,
                        bikeModels.Find(obj => obj.id == BikeModelId).price
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

            (int bikeModelId, string size) = GetBikeModelIdAndSize(IdBike);
            List<string> references = GetReferencesByBikeModelIdAndSize(bikeModelId, size);

            foreach (string reference in references)
            {
                DecrementPart(reference);
            }
        }

        public void DecrementPart(string reference)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateSql = "UPDATE dbNicebike.part SET Quantity = Quantity - 1 WHERE Ref = @reference";

                MySqlCommand updateCommand = new MySqlCommand(updateSql, connection);
                updateCommand.Parameters.AddWithValue("@reference", reference);
                updateCommand.ExecuteNonQuery();
            }
        }

        private (int bikeModelId, string size) GetBikeModelIdAndSize(int idBike)
        {
            int bikeModelId = 0;
            string size = string.Empty;

            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT BikeModel, Size FROM dbNicebike.bike WHERE IdBike = @idBike";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@idBike", idBike);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                bikeModelId = reader.GetInt32("BikeModel");
                size = reader.GetString("Size");
            }

            return (bikeModelId, size);
        }

        private List<string> GetReferencesByBikeModelIdAndSize(int bikeModelId, string size)
        {
            if (bikeModelId == 1) // city
            {
                if (size == "26\"")
                    return new List<string> { "FRA12", "FRA01", "BRA01", "CHA01", "CHA02", "DER02", "BRA02", "FRA02", "FRA03", "FRA04", "TYR03", "FRA05", "CHAO3", "TYR04", "TYR05", "FRA06", "FRA07", "FRA08", "TYR06", "TYR07", "TYR08" };
                else if (size == "28\"")
                    return new List<string> { "FRA12", "FRA11", "BRA01", "CHA01", "CHA02", "DER02", "BRA02", "FRA02", "FRA03", "FRA04", "TYR03", "FRA05", "CHAO3", "TYR04", "TYR05", "FRA06", "FRA07", "FRA08", "TYR06", "TYR07", "TYR08" };

            }
            else if (bikeModelId == 2) // explorer
            {
                if (size == "26\"")
                    return new List<string> { "FRA12", "FRA01", "BRA01", "CHA01", "CHA02", "DER02", "BRA02", "FRA02", "FRA03", "TYR02", "TYR01", "FRA04", "FRA05", "CHAO3", "TYR04", "TYR05", "FRA06", "FRA07", "FRA08", "TYR07", "TYR08" };
                else if (size == "28\"")
                    return new List<string> { "FRA12", "FRA11", "BRA01", "CHA01", "CHA02", "DER02", "BRA02", "FRA02", "FRA03", "TYR02", "TYR01", "FRA04", "FRA05", "CHAO3", "TYR04", "TYR05", "FRA06", "FRA07", "FRA08", "TYR07", "TYR08" };
            }
            else if (bikeModelId == 3) // adventure
            {
                if (size == "26\"")
                    return new List<string> { "BRA01", "CHA01", "CHA02", "DER02", "BRA02", "FRA02", "FRA03", "FRA05", "CHAO3", "TYR04", "TYR05", "FRA06", "FRA07", "FRA08", "FRA09", "TYR07", "TYR08" };
                else if (size == "28\"")
                    return new List<string> { "BRA01", "CHA01", "CHA02", "DER02", "BRA02", "FRA02", "FRA03", "FRA05", "CHAO3", "TYR04", "TYR05", "FRA06", "FRA07", "FRA08", "FRA10", "TYR07", "TYR08" };

            }

            return new List<string>(); // Retourne une liste vide si le BikeModelId ou la taille ne sont pas reconnus
        }
    }
}


