using System;
using MySql.Data.MySqlClient;
using Nicebike.Models;
namespace Nicebike.ViewModels
{
	public class ModifyDataSupplier
	{
        public void modifySupplier(int id, string name, string mail, string phone, string street, string town, string number)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "UPDATE dbNicebike.suppliers SET Name = @name, Mail = @mail, Phone = @phone, Street = @street, Town = @town, Number = @number WHERE IdSupplier = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@mail", mail);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@street", street);
            command.Parameters.AddWithValue("@town", town);
            command.Parameters.AddWithValue("@number", number);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}

