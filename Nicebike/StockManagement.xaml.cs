namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using System.Collections.ObjectModel;

public partial class StockManagement : ContentPage
{
    public StockManagement()
    {
        InitializeComponent();

        // Créer une instance de la classe SupplierManagement
        PartsManagement stockManagement = new PartsManagement();

       // ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
        ObservableCollection<Part> observableParts = stockManagement.GetAllParts();

        // Assigner la liste des fournisseurs à la source de données du ListView
        partListView.ItemsSource = observableParts;
    }

    private async void NewPart(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PartDatasheet());
    }

    private async void OnModifyClickedPart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var part = (Part)button.BindingContext;
        var modifyPage = new ModifyPart(part);

        await Navigation.PushAsync(modifyPage);
    }

    private void DeletePart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdPart = (int)button.CommandParameter;

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.DeletePart(IdPart);
    }
}

public class PartsManagement
{
    public ObservableCollection<Part> GetAllParts()
    {
        ObservableCollection<Part> parts = new ObservableCollection<Part>();

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

    public void SendPart(List <Supplier> suppliers, Entry reference, Entry description, Entry quantity, Entry threshold, Picker supplier)
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
        command.Parameters.AddWithValue("@supplier", suppliers[supplier.SelectedIndex].idSupplier);

        command.ExecuteNonQuery();
    }

    public void DeletePart(int IdPart)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        
        string sql = "DELETE FROM dbNicebike.part WHERE idPart = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", IdPart);

        command.ExecuteNonQuery();
    }
}
