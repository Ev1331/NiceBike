using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
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
}

