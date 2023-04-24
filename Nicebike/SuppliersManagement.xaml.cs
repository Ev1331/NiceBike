namespace Nicebike;

using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.Models;


public partial class SuppliersManagement : ContentPage //sert à afficher les données
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
    public void OnConfirmClicked(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameEntry"); 
        Entry mail = this.FindByName<Entry>("mailEntry"); 
        Entry phone = this.FindByName<Entry>("phoneEntry");  
        Entry street = this.FindByName<Entry>("streetEntry"); 
        Entry number = this.FindByName<Entry>("numberEntry"); 
        Entry town = this.FindByName<Entry>("townEntry"); 

        SupplierManagement supplierManagement = new SupplierManagement();

        supplierManagement.SendSupplier(name, mail, phone, street, town, number);

    }
    public void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idSupplier = (int)button.CommandParameter;

        SupplierManagement supplierManagement = new SupplierManagement();
        supplierManagement.DeleteSupplier(idSupplier);


        
    }


}

public class SupplierManagement //sert à traiter les données
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
    public void SendSupplier(Entry name, Entry mail, Entry phone, Entry street, Entry town, Entry number)
    {
        

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";


        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO dbNicebike.suppliers (Name, Mail, Phone, Street, Town, Number) VALUES (@name, @mail, @phone, @street, @town, @number)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name.Text);
        command.Parameters.AddWithValue("@mail", mail.Text);
        command.Parameters.AddWithValue("@phone", phone.Text);
        command.Parameters.AddWithValue("@street", street.Text);
        command.Parameters.AddWithValue("@town", town.Text);
        command.Parameters.AddWithValue("@number", number.Text);

        command.ExecuteNonQuery();


    
    }
    public void DeleteSupplier(int idSupplier)

    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        
        string sql = "DELETE FROM dbNicebike.suppliers WHERE idSupplier = @id";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", idSupplier);

        command.ExecuteNonQuery();

    }


}
