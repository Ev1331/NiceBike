namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;

public partial class EmployeeMgmt : ContentPage
{
	public EmployeeMgmt()
	{
		InitializeComponent();

        EmployeeManagement employeeManagement = new EmployeeManagement();

        List<Employee> employees = employeeManagement.GetAllEmployee();

        employeeListView.ItemsSource = employees;
    }

    public void OnConfirmClicked(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameEntry");
        Entry surname = this.FindByName<Entry>("surnameEntry");
        Entry mail = this.FindByName<Entry>("mailEntry");
        Entry function = this.FindByName<Entry>("functionEntry");
        Entry phone = this.FindByName<Entry>("phoneEntry");

        EmployeeManagement employeeManagement = new EmployeeManagement();

        employeeManagement.SendEmployee(name, surname, mail, function, phone);


    }

    public void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idEmployee = (int)button.CommandParameter;

        EmployeeManagement employeeManagement = new EmployeeManagement();
        employeeManagement.DeleteEmployee(idEmployee);



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
            Employee employee = new Employee(
                reader.GetInt32("IdEmployee"),
                reader.GetString("Name"),
                reader.GetString("Surname"),
                reader.GetString("Mail"),
                reader.GetString("Function"),
                reader.GetString("Phone")
                
            );


            employees.Add(employee);
        }


        return employees;
    }

    public void SendEmployee(Entry name, Entry surname, Entry mail, Entry function, Entry phone)
    {


        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";


        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO dbNicebike.employee (Name, Surname, Mail, Function, Phone) VALUES (@name, @surname, @mail, @function, @phone)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name.Text);
        command.Parameters.AddWithValue("@surname", surname.Text);
        command.Parameters.AddWithValue("@mail", mail.Text);
        command.Parameters.AddWithValue("@function", function.Text);
        command.Parameters.AddWithValue("@phone", phone.Text);

        command.ExecuteNonQuery();
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

    }

}