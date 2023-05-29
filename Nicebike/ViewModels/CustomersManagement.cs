using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class CustomersManagement
	{
        private MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        private string sql;
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            connection.Open();

            sql = "SELECT * FROM dbNicebike.customer";
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
            connection.Open();

            sql = "INSERT INTO dbNicebike.customer (Name, Surname, Mail, Phone, Street, Town, Number) VALUES (@name, @surname, @mail, @phone, @street, @town, @number)";
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

            sql = "DELETE FROM dbNicebike.customer WHERE idCustomer = @id";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", idCustomer);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ModifyCustomer(int id, string name, string surname, string mail, string phone, string street, string town, string number)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            sql = "UPDATE dbNicebike.customer SET Name = @name, Surname = @surname, Mail = @mail, Phone = @phone, Street = @street, Town = @town, Number = @number WHERE IdCustomer = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@mail", mail);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@street", street);
            command.Parameters.AddWithValue("@town", town);
            command.Parameters.AddWithValue("@number", number);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}