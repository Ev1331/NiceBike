namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;

public partial class EmployeeMgmt : ContentPage
{
    EmployeeManagement employeeManagement;

	public EmployeeMgmt()
	{
		InitializeComponent();

        this.employeeManagement = new EmployeeManagement();

        List<Employee> employees = employeeManagement.GetAllEmployee();

        employeeListView.ItemsSource = employees;
    }

    public void OnConfirmClickedEmployee(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameEntry");
        Entry surname = this.FindByName<Entry>("surnameEntry");
        Entry mail = this.FindByName<Entry>("mailEntry");
        Entry jobtitle = this.FindByName<Entry>("jobtitleEntry");
        Entry phone = this.FindByName<Entry>("phoneEntry");

        this.employeeManagement.SendEmployee(name, surname, mail, jobtitle, phone);

        List<Employee> employees = employeeManagement.GetAllEmployee();

        employeeListView.ItemsSource = employees;

    }

    public void OnDeleteClickedEmployee(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idEmployee = (int)button.CommandParameter;

        this.employeeManagement.DeleteEmployee(idEmployee);

        List<Employee> employees = employeeManagement.GetAllEmployee();

        employeeListView.ItemsSource = employees;

    }

    public async void OnModifyClickedEmployee(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var employee = (Employee)button.BindingContext;


        var modifyPage = new ModifyEmployee(employee);


        await Navigation.PushAsync(modifyPage);

        Navigation.RemovePage(this);

    }

}

public class EmployeeManagement //sert à traiter les données
{
    public List<Employee> GetAllEmployee()
    {
        List<Employee> employees = new List<Employee>();


        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.employee";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();


        while (reader.Read())
        {
            if (reader.GetString("IdEmployee") != "0") {
                Employee employee = new Employee(
                reader.GetInt32("IdEmployee"),
                reader.GetString("Name"),
                reader.GetString("Surname"),
                reader.GetString("Mail"),
                reader.GetString("JobTitle"),
                reader.GetString("Phone"));

                employees.Add(employee);
            }
                
            
        }

        connection.Close();
        return employees;
    }

    public void SendEmployee(Entry name, Entry surname, Entry mail, Entry jobtitle, Entry phone)
    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO dbNicebike.employee (Name, Surname, Mail, JobTitle, Phone) VALUES (@name, @surname, @mail, @jobtitle, @phone)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name.Text);
        command.Parameters.AddWithValue("@surname", surname.Text);
        command.Parameters.AddWithValue("@mail", mail.Text);
        command.Parameters.AddWithValue("@jobtitle", jobtitle.Text);
        command.Parameters.AddWithValue("@phone", phone.Text);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public void DeleteEmployee(int idEmployee)

    {
        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM dbNicebike.employee WHERE idEmployee = @id";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", idEmployee);

        command.ExecuteNonQuery();
        connection.Close();

    }

}