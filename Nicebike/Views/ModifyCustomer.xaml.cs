namespace Nicebike.Views;
using Nicebike.Models;
using MySql.Data.MySqlClient;

public partial class ModifyCustomer : ContentPage
{
    Customer customer;
    int customerId;

	public ModifyCustomer(Customer customer)
	{
		InitializeComponent();
        this.customer = customer;
        customerId = customer.idCustomer;

        BindingContext = customer;
	}


    public async void modifyClickedCustomer(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry surname = this.FindByName<Entry>("surnameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Entry phone = this.FindByName<Entry>("phoneChange");
        Entry street = this.FindByName<Entry>("streetChange");
        Entry town = this.FindByName<Entry>("townChange");
        Entry number = this.FindByName<Entry>("numberChange");

        ModifyData modifyData = new ModifyData();
        modifyData.modifyCustomer(customerId, name.Text, surname.Text, mail.Text, phone.Text, street.Text, town.Text, number.Text);

        await Navigation.PushAsync(new ClientsManagement());
        Navigation.RemovePage(this);

    }
}


public class ModifyData
{
    public void modifyCustomer(int id, string name, string surname, string mail, string phone, string street, string town, string number)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE dbNicebike.customer SET Name = @name, Surname = @surname, Mail = @mail, Phone = @phone, Street = @street, Town = @town, Number = @number WHERE IdCustomer = @id";
        MySqlCommand command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@surname", surname);
        command.Parameters.AddWithValue("@mail", mail);
        command.Parameters.AddWithValue("@phone", phone);
        command.Parameters.AddWithValue("@street", street);
        command.Parameters.AddWithValue("@town", town);
        command.Parameters.AddWithValue("@number", number);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }
}
