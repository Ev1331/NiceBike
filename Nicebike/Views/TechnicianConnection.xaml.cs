using MySql.Data.MySqlClient;

namespace Nicebike.Views;
using Nicebike.Models;



public partial class TechnicianConnection : ContentPage
{
    public List<Employee> Employees { get; set; }
    public TechnicianConnection()
	{
		InitializeComponent();
        Employees = GetEmployees();
        BindingContext = this;
	}

    private async void NavigateToTechnician(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idTechnician = Convert.ToInt32(button.CommandParameter);
        //Navigation.PushAsync(new TechnicianHome(idTechnician));
        var homeTechnician = new TechnicianHome(idTechnician);


       Navigation.PushAsync(homeTechnician);
    }

    public List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();

        string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM dbNicebike.employee WHERE JobTitle = 'Technician'";

        using MySqlCommand command = new MySqlCommand(sql, connection);

        using MySqlDataReader reader = command.ExecuteReader();



        while (reader.Read())
        {
            string jobTitle = reader.GetString("JobTitle");
            if (jobTitle == "Technician")
            {
                Employee employee = new Employee(
                    reader.GetInt32("IdEmployee"),
                    reader.GetString("Name"),
                    reader.GetString("Surname"),
                    reader.GetString("Mail"),
                    jobTitle,
                    reader.GetString("Phone")
                );
                employees.Add(employee);
            }
        }
        return employees;
    }

}


