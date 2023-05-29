using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class SupplierManagement
    {
        private MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        public string sql;
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            connection.Open();
            string sql = "SELECT * FROM dbNicebike.suppliers";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Supplier supplier = new Supplier(
                    reader.GetInt32("IdSupplier"),
                    reader.GetString("name"),
                    reader.GetString("mail"),
                    reader.GetString("phone"),
                    reader.GetString("street"),
                    reader.GetString("town"),
                    reader.GetString("number")
                );
                suppliers.Add(supplier);
            }
            return suppliers;
        }

        public void SendSupplier(Entry name, Entry mail, Entry phone, Entry street, Entry town, Entry number)
        {
            connection.Open();

            sql = "INSERT INTO dbNicebike.suppliers (Name, Mail, Phone, Street, Town, Number) VALUES (@name, @mail, @phone, @street, @town, @number)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name.Text);
            command.Parameters.AddWithValue("@mail", mail.Text);
            command.Parameters.AddWithValue("@phone", phone.Text);
            command.Parameters.AddWithValue("@street", street.Text);
            command.Parameters.AddWithValue("@town", town.Text);
            command.Parameters.AddWithValue("@number", number.Text);

            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteSupplier(int idSupplier)
        {
            connection.Open();

            sql = "DELETE FROM dbNicebike.suppliers WHERE idSupplier = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", idSupplier);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ModifySupplier(int id, string name, string mail, string phone, string street, string town, string number)
        {
            connection.Open();

            sql = "UPDATE dbNicebike.suppliers SET Name = @name, Mail = @mail, Phone = @phone, Street = @street, Town = @town, Number = @number WHERE IdSupplier = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name);
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