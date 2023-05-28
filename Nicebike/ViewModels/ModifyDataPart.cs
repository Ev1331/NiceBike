using System;
using Nicebike.Models;
using MySql.Data.MySqlClient;
namespace Nicebike.ViewModels
{
	public class ModifyDataPart
	{
        public void ModifyPart(int id, List<Supplier> suppliers, Entry reference, Entry description, Entry quantity, Entry threshold, int IdSupplier)
        {
            string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "UPDATE dbNicebike.part SET Ref = @reference, Description = @description, Quantity = @quantity, Threshold = @threshold, Supplier = @IdSupplier WHERE IdPart = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@reference", reference.Text);
            command.Parameters.AddWithValue("@description", description.Text);
            command.Parameters.AddWithValue("@quantity", quantity.Text);
            command.Parameters.AddWithValue("@threshold", threshold.Text);
            command.Parameters.AddWithValue("@IdSupplier", IdSupplier);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}

