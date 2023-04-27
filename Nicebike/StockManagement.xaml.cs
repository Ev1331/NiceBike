namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;

public partial class StockManagement : ContentPage
{
    public StockManagement()
    {
        InitializeComponent();

        // Créer une instance de la classe SupplierManagement
        PartsManagement stockManagement = new PartsManagement();

        // Récupérer la liste des fournisseurs à partir de la base de données
        List<Part> parts = stockManagement.GetAllParts();

        // Assigner la liste des fournisseurs à la source de données du ListView
        partListView.ItemsSource = parts;
    }

    private async void NewPart(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PartDatasheet());
    }

}

public class PartsManagement
{
    public List<Part> GetAllParts()
    {
        List<Part> parts = new List<Part>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.part";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Part part = new Part(
                reader.GetInt32("IdPart"),
                reader.GetString("Ref"),
                reader.GetString("Description"),
                reader.GetInt32("Quantity"),
                reader.GetInt32("Threshold"),
                reader.GetInt32("Supplier")
            );
            parts.Add(part);
        }
        return parts;
    }

    public void SendPart(Entry reference, Entry description, Entry quantity, Entry threshold, Entry supplier)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO dbNicebike.part (Ref, Description, Quantity, Threshold, Supplier) VALUES (@reference, @description, @quantity, @threshold, @supplier)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@reference", reference.Text);
        command.Parameters.AddWithValue("@description", description.Text);
        command.Parameters.AddWithValue("@quantity", quantity.Text);
        command.Parameters.AddWithValue("@threshold", threshold.Text);
        command.Parameters.AddWithValue("@supplier", supplier.Text);

        command.ExecuteNonQuery();
    }

}
