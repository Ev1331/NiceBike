using System;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using System.Collections.ObjectModel;


namespace Nicebike.ViewModels
{
	public class PartsManagement
	{
        private MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
        private string sql;
        public List<Part> GetAllParts()
        {
            SupplierManagement supplierManagement = new SupplierManagement();
            List<Supplier> suppliers = supplierManagement.GetAllSuppliers();
            List<Part> parts = new List<Part>();
            int idSupplier;

            connection.Open();

            sql = "SELECT * FROM dbNicebike.part";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                idSupplier = reader.GetInt32("Supplier");
                Part part = new Part(
                    reader.GetInt32("IdPart"),
                    reader.GetString("Ref"),
                    reader.GetString("Description"),
                    reader.GetInt32("Quantity"),
                    reader.GetInt32("Threshold"),
                    idSupplier,
                    suppliers.Find(obj => obj.idSupplier == idSupplier).name
                );
                parts.Add(part);
            }
            connection.Close();
            return parts;
        }

        public void SendPart(List<Supplier> suppliers, Entry reference, Entry description, Entry quantity, Entry threshold, int IdSupplier)
        {
            connection.Open();

            sql = "INSERT INTO dbNicebike.part (Ref, Description, Quantity, Threshold, Supplier) VALUES (@reference, @description, @quantity, @threshold, @IdSupplier)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@reference", reference.Text);
            command.Parameters.AddWithValue("@description", description.Text);
            command.Parameters.AddWithValue("@quantity", quantity.Text);
            command.Parameters.AddWithValue("@threshold", threshold.Text);
            command.Parameters.AddWithValue("@IdSupplier", IdSupplier);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeletePart(int IdPart)
        {
            connection.Open();

            sql = "DELETE FROM dbNicebike.part WHERE idPart = @id";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", IdPart);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddQuantity(int IdPart, Entry quantity)
        {
            connection.Open();
            sql = "UPDATE dbNicebike.part SET Quantity = Quantity + @AddedQuantity WHERE IdPart = @IdPart";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdPart", IdPart);
            command.Parameters.AddWithValue("@AddedQuantity", quantity.Text);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveQuantity(int IdPart, Entry quantity)
        {
            connection.Open();
            sql = "UPDATE dbNicebike.part SET Quantity = Quantity - @AddedQuantity WHERE IdPart = @IdPart";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdPart", IdPart);
            command.Parameters.AddWithValue("@AddedQuantity", quantity.Text);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RestockAll(ObservableCollection<Part> lowParts)
        {
            connection.Open();

            foreach (Part part in lowParts)
            {
                sql = "UPDATE dbNicebike.part SET Quantity = Quantity + @threshold WHERE IdPart = @IdPart";

                using MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdPart", part.id);
                command.Parameters.AddWithValue("@threshold", part.threshold);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

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