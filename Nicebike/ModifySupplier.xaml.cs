namespace Nicebike;
using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.Models;




public partial class ModifySupplier : ContentPage
{
    Supplier supplier;
    int supplierId;

    public ModifySupplier(Supplier supplier)
    {
        InitializeComponent();
        this.supplier = supplier;
        supplierId = supplier.idSupplier;

        // Définir le contexte de liaison des données
        BindingContext = supplier;
    }

    public void modifyClicked(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Entry phone = this.FindByName<Entry>("phoneChange");
        Entry street = this.FindByName<Entry>("streetChange");
        Entry number = this.FindByName<Entry>("numberChange");
        Entry town = this.FindByName<Entry>("townChange");

        ModifyData modifyData = new ModifyData();
        modifyData.modifySupplier(supplierId, name.Text, mail.Text, phone.Text, street.Text, town.Text, number.Text);
    }
}

public class ModifyData
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

