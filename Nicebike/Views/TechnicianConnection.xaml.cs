using MySql.Data.MySqlClient;

namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;



public partial class TechnicianConnection : ContentPage
{
    public List<Employee> Employees { get; set; }
    EmployeeManagement employeeManagement = new EmployeeManagement();
    public TechnicianConnection()
	{
		InitializeComponent();
        Employees = employeeManagement.GetTechnician();
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

    

}


