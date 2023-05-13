namespace Nicebike.Views;
using Nicebike.Models;
using MySql.Data.MySqlClient;


public partial class ModifyEmployee : ContentPage
{
    Employee employee;
    int employeeId;

	public ModifyEmployee(Employee employee)
	{
		InitializeComponent();

        this.employee = employee;
        employeeId = employee.idEmployee;

        BindingContext = employee;
    }

    public async void modifyClickedEmployee(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry surname = this.FindByName<Entry>("surnameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Entry jobtitle = this.FindByName<Entry>("jobtitleChange");
        Entry phone = this.FindByName<Entry>("phoneChange");

        ModifyDataEmployee modifyDataEmployee = new ModifyDataEmployee();
        modifyDataEmployee.modifyEmployee(employeeId, name.Text, surname.Text, mail.Text, jobtitle.Text, phone.Text);

        await Navigation.PushAsync(new EmployeeMgmt());
        Navigation.RemovePage(this);

    }
}

public class ModifyDataEmployee
{
    public void modifyEmployee(int id, string name, string surname, string mail, string jobtitle, string phone)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE dbNicebike.employee SET Name = @name, Surname = @surname, Mail = @mail, JobTitle = @jobtitle, Phone = @phone WHERE IdEmployee = @id";
        MySqlCommand command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@surname", surname);
        command.Parameters.AddWithValue("@mail", mail);
        command.Parameters.AddWithValue("@jobtitle", jobtitle);
        command.Parameters.AddWithValue("@phone", phone);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }
}