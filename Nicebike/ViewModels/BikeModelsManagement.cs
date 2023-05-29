using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class BikeModelsManagement
	{
        private MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        private string sql;
        public List<BikeModel> GetAllBikeModels()
        {
            List<BikeModel> bikeModels = new List<BikeModel>();

            connection.Open();

            sql = "SELECT * FROM dbNicebike.bikemodel";
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
            connection.Close();

            return bikeModels;
        }
    }
}