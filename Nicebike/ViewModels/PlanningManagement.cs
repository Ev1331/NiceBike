using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Nicebike.ViewModels
{
	public class PlanningManagement
	{
        private MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        public string sql;
        public CustomersManagement customersManagement = new CustomersManagement();
        public List<Order> GetOrders()
        {
            List<Customer> customerList = customersManagement.GetAllCustomers();
            List<Order> orderList = new List<Order>();
            string customerName;
            string productionDate;
            int id;
            
            connection.Open();
            sql = "SELECT * FROM dbNicebike.order";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString("Status") != "Done")
                {
                    id = reader.GetInt32("CustomerID");
                    string deliveryDate = reader.GetString("DeliveryDate");
                    DateTime date = DateTime.ParseExact(deliveryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime newDate = date.AddDays(-3);
                    productionDate = newDate.ToString("yyyy-MM-dd");
                    customerName = customerList.Find(obj => obj.idCustomer == reader.GetInt32("CustomerID")).surname + " " + customerList.Find(obj => obj.idCustomer == reader.GetInt32("CustomerID")).name;
                    Order order = new Order(
                                reader.GetInt32("IdOrder"),
                                id,
                                reader.GetString("Date"),
                                productionDate,
                                reader.GetString("Status"),
                                customerName
                            );
                    orderList.Add(order);
                }
            }
            connection.Close();
            return orderList;
        }

        public void ModifyProductionDate(int id, string productionDate)
        {
            connection.Open();
            DateTime date = DateTime.ParseExact(productionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime newDate = date.AddDays(3);
            string deliveryDate = newDate.ToString("yyyy-MM-dd");

            string sql = "UPDATE dbNicebike.order SET DeliveryDate = @deliveryDate WHERE IdOrder = @IdOrder";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@deliveryDate", deliveryDate);
            command.Parameters.AddWithValue("@IdOrder", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}