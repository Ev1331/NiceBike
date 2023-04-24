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
}