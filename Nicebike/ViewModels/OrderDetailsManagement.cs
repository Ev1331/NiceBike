using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class OrderDetailsManagement
	{
        private MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        private string sql;

        private List<int> bikesIdList = new List<int>();
        private List<Bike> orderBikes = new List<Bike>();
        private int id;
        public List<Bike> GetOrderBikes(int IdOrder)
        {
            BikesManagement bikesManagement = new BikesManagement();
            List<Bike> bikes = bikesManagement.GetAllBikes();

            connection.Open();
            sql = "SELECT * FROM dbNicebike.orderdetails WHERE IdOrder = @IdOrder";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdOrder", IdOrder);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt32("Bike");
                bikesIdList.Add(id);
                orderBikes.Add(bikes.Find(obj => obj.id == id)); //Monte une liste avec les v�los correspondants
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

        public int GetOrderPrice(List<Bike> bikes)
        {
            int totalPrice = 0;
            foreach(Bike bike in bikes)
            {
                totalPrice += bike.bikePrice;
            }

            return totalPrice;
        }
    }
}