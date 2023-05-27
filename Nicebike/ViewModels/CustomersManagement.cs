using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class CustomersManagement
	{
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM dbNicebike.customer";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Customer customer = new Customer(
                    reader.GetInt32("IdCustomer"),
                    reader.GetString("Name"),
                    reader.GetString("Surname"),
                    reader.GetString("Mail"),
                    reader.GetString("Phone"),
                    reader.GetString("Street"),
                    reader.GetString("Town"),
                    reader.GetString("Number"));

                customers.Add(customer);
            }
            connection.Close();
            return customers;
        }

        public void SendCustomer(Entry name, Entry surname, Entry mail, Entry phone, Entry street, Entry town, Entry number)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "INSERT INTO dbNicebike.customer (Name, Surname, Mail, Phone, Street, Town, Number) VALUES (@name, @surname, @mail, @phone, @street, @town, @number)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name.Text);
            command.Parameters.AddWithValue("@surname", surname.Text);
            command.Parameters.AddWithValue("@mail", mail.Text);
            command.Parameters.AddWithValue("@phone", phone.Text);
            command.Parameters.AddWithValue("@street", street.Text);
            command.Parameters.AddWithValue("@town", town.Text);
            command.Parameters.AddWithValue("@number", number.Text);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteCustomer(int idCustomer)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "DELETE FROM dbNicebike.customer WHERE idCustomer = @id";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", idCustomer);

            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Customer> SearchCustomer()
        {
            CustomersManagement customersManagement = new CustomersManagement();
            List<Customer> customersList = customersManagement.GetAllCustomers();

            return customersList;
        }
    }
}

