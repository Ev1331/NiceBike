using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Nicebike.ViewModels
{
	public class BikesManagement
	{
        int IdBike;
        int IdOrder;
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        int BikecountPlus = 0;
        int BikecountMinus = 6;

        
        

        public List<Bike> GetAllBikes()
        {
            BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
            OrderManagement orderManagement = new OrderManagement();
            OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();
            List<BikeModel> bikeModels = new List<BikeModel>();
            bikeModels = bikeModelsManagement.GetAllBikeModels();
            int BikeModelId;
            List<Bike> bikes = new List<Bike>();
            List<Order> order = new List<Order>();
            order = orderManagement.GetAllOrders();

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM dbNicebike.bike";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
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
                bikes.Add(bike);
            }
            return bikes;
        }
        public void DeleteBike(int IdBike)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "DELETE FROM dbNicebike.orderdetails WHERE Bike = @id";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", IdBike);

            command.ExecuteNonQuery();

            sql = "DELETE FROM dbNicebike.bike WHERE IdBike = @id";
            using MySqlCommand command2 = new MySqlCommand(sql, connection);
            command2.Parameters.AddWithValue("@id", IdBike);

            command2.ExecuteNonQuery();





        }
        public void SendBike(string[] colorList, string[] sizeList, List<BikeModel> bikeModels, Picker color, string type, Picker size, string reference, Picker bikeModel, string status, int IdOrder)
        {
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

            sql = "SELECT IdBike FROM dbNicebike.bike ORDER BY IdBike DESC LIMIT 1";
            MySqlCommand command2 = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command2.ExecuteReader();

            while (reader.Read())
            {
                IdBike = reader.GetInt32("IdBike");
            }

            using MySqlConnection connection2 = new MySqlConnection(connectionString);
            connection2.Open();
            sql = "INSERT INTO dbNicebike.orderdetails (IdOrder, Bike) VALUES (@IdOrder, @IdBike)";
            MySqlCommand command3 = new MySqlCommand(sql, connection2);
            command3.Parameters.AddWithValue("@IdOrder", IdOrder);
            command3.Parameters.AddWithValue("@IdBike", IdBike);
            command3.ExecuteNonQuery();


            BikecountPlus++;
            if (BikecountPlus == 6)
            {
                BikecountPlus = 0;
                string newDeliveryDate = "";
                using MySqlConnection connection3 = new MySqlConnection(connectionString);
                connection3.Open();
                sql = "SELECT * FROM dbNicebike.order WHERE IdOrder = @IdOrder";
                using MySqlCommand command4 = new MySqlCommand(sql, connection3);
                command4.Parameters.AddWithValue("@IdOrder", IdOrder);
                using MySqlDataReader reader1 = command4.ExecuteReader();

                while (reader1.Read())
                {
                    string DeleveryDate = reader1.GetString("DeliveryDate");
                    DateTime date = DateTime.ParseExact(DeleveryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime newDate = date.AddDays(1);
                    newDeliveryDate = newDate.ToString("yyyy-MM-dd");
                }

                using MySqlConnection connection4 = new MySqlConnection(connectionString);
                connection4.Open();
                sql = "UPDATE dbNicebike.order SET DeliveryDate = @newDeliveryDate WHERE IdOrder = @IdOrder";
                MySqlCommand command5 = new MySqlCommand(sql, connection4);
                command5.Parameters.AddWithValue("@newDeliveryDate", newDeliveryDate);
                command5.Parameters.AddWithValue("@IdOrder", IdOrder);

                command5.ExecuteNonQuery();

            }
        }
    }
}


