using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class ModifyDataEmployee
	{
        public void modifyEmployee(string[] jobTitleList, int id, string name, string surname, string mail, Picker jobtitle, string phone)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "UPDATE dbNicebike.employee SET Name = @name, Surname = @surname, Mail = @mail, JobTitle = @jobtitle, Phone = @phone WHERE IdEmployee = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@mail", mail);
            command.Parameters.AddWithValue("@jobtitle", jobTitleList[jobtitle.SelectedIndex]);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}

