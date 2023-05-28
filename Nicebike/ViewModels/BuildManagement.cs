using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;

namespace Nicebike.ViewModels
{
	public class BuildManagement
	{
        private BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
        private OrderManagement orderManagement = new OrderManagement();
        private OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();

        private string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        public List<Bike> BikesForBuilder(int id)
        {
            
            List<BikeModel> bikeModels = new List<BikeModel>();
            bikeModels = bikeModelsManagement.GetAllBikeModels();
            int BikeModelId;

            List<Bike> bikesForBuilder = new List<Bike>();

            List<Order> order = new List<Order>();
            order = orderManagement.GetAllOrders();

            

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM dbNicebike.bike";

            using MySqlCommand command = new MySqlCommand(sql, connection);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString("Status") == "InProgress" && reader.GetInt32("Technician") == id)
                {
                    int IdOrder = orderDetailsManagement.GetAssociatedOrderId(reader.GetInt32("IdBike"));
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
                        bikeModels.Find(obj => obj.id == BikeModelId).description,
                        bikeModels.Find(obj => obj.id == BikeModelId).price,
                        order.Find(obj => obj.IdOrder == IdOrder).DeliveryDate
                    );

                    bikesForBuilder.Add(bike);
                }
            }

            return bikesForBuilder;


        }
        public void FinishBike(int IdBike, int IdTechnician)
        {

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "UPDATE dbNicebike.bike SET Technician = @technician, Status = 'Done' WHERE IdBike = @IdBike";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@technician", IdTechnician);
            command.Parameters.AddWithValue("@IdBike", IdBike);

            command.ExecuteNonQuery();
            connection.Close();

            //If all bikes are done, the order is don
            int IdOrder = orderDetailsManagement.GetAssociatedOrderId(IdBike);
            List<Bike> orderBikes = orderDetailsManagement.GetOrderBikes(IdOrder);
            int bikesDone = 0;

            foreach (Bike bike in orderBikes)
            {
                if (bike.status == "Done")
                {
                    bikesDone++;
                }
            }
            if (bikesDone == orderBikes.Count)
            {
                connection.Open();
                sql = "UPDATE dbNicebike.order SET Status = 'Done' WHERE IdOrder = @IdOrder";
                using MySqlCommand command2 = new MySqlCommand(sql, connection);
                command2.Parameters.AddWithValue("@IdOrder", IdOrder);
                command2.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

