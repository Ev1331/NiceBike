namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

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

