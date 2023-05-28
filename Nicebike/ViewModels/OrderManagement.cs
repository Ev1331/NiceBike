using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Nicebike.ViewModels
{
	public class OrderManagement
	{
        public CustomersManagement customersManagement = new CustomersManagement();
        public BikesManagement bikesManagement = new BikesManagement();
        public OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();

        MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        public string sql;
        string customerName;
        int id;
        public List<Order> GetAllOrders()
        {
            List<Order> orderList = new List<Order>();
            List<Customer> customerList = new List<Customer>();
            customerList = customersManagement.GetAllCustomers();

            connection.Open();
            sql = "SELECT * FROM dbNicebike.order";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt32("CustomerID");
                customerName = customerList.Find(obj => obj.idCustomer == reader.GetInt32("CustomerID")).surname + " " + customerList.Find(obj => obj.idCustomer == reader.GetInt32("CustomerID")).name;
                Order order = new Order(
                            reader.GetInt32("IdOrder"),
                            id,
                            reader.GetString("Date"),
                            reader.GetString("DeliveryDate"),
                            reader.GetString("Status"),
                            customerName
                        );
                orderList.Add(order);
            }
            connection.Close();
            return orderList;
        }

        public void DeleteOrder(int IdOrder)
        {
            List<Bike> orderBikes = orderDetailsManagement.GetOrderBikes(IdOrder);

            //Delete every bike from this order and every orderdetail associated
            foreach (Bike bike in orderBikes)
            {
                bikesManagement.DeleteBike(bike.id);
            }

            //Finally, deletes the order itself

            connection.Open();
            sql = "DELETE FROM dbNicebike.order WHERE IdOrder = @IdOrder";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdOrder", IdOrder);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void CreateOrder(int IdCustomer)
        {
            DateTime date = DateTime.Now;
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd");

            connection.Open();
            sql = "INSERT INTO dbNicebike.order (customerId, Date, DeliveryDate, Status) VALUES (@IdCustomer, @Date, @DeliveryDate, @Status)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdCustomer", IdCustomer);
            command.Parameters.AddWithValue("@Date", formattedDateTime);
            command.Parameters.AddWithValue("@DeliveryDate", DeliveryDate());
            command.Parameters.AddWithValue("@Status", "Waiting");
            command.ExecuteNonQuery();
            connection.Close();
        }

        public String DeliveryDate()
        {

            sql = "SELECT * FROM dbNicebike.order ORDER BY STR_TO_DATE(DeliveryDate, '%Y-%m-%d') DESC LIMIT 1";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            string deliveryDate = "";


            while (reader.Read())
            {
                string latestDeleveryDate = reader.GetString("DeliveryDate");
                DateTime date = DateTime.ParseExact(latestDeleveryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime newDate = date.AddDays(1);
                deliveryDate = newDate.ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(deliveryDate))
            {
                // Aucune date de livraison trouvée, utiliser la date actuelle + 1 jour
                DateTime currentDate = DateTime.Now.Date;
                DateTime newDate = currentDate.AddDays(1 + 3);
                deliveryDate = newDate.ToString("yyyy-MM-dd");
            }

            return deliveryDate;
        }
    }
}

