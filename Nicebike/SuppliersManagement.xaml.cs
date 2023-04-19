namespace Nicebike;

using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.Models;

public partial class SuppliersManagement : ContentPage
{
    public SuppliersManagement()
    {
        InitializeComponent();

        // Créer une instance de la classe SupplierManagement
        SupplierManagement supplierManagement = new SupplierManagement();

        // Récupérer la liste des fournisseurs à partir de la base de données
        List<Supplier> suppliers = supplierManagement.GetAllSuppliers();

        // Assigner la liste des fournisseurs à la source de données du ListView
        supplierListView.ItemsSource = suppliers;
    }
}

public class SupplierManagement
{
    public List<Supplier> GetAllSuppliers()
    {
        List<Supplier> suppliers = new List<Supplier>();

        
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        
        using MySqlConnection connection = new MySqlConnection(connectionString);
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
}
