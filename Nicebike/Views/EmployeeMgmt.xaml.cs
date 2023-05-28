namespace Nicebike.Views;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class EmployeeMgmt : ContentPage
{
    EmployeeManagement employeeManagement;
    string[] jobTitleList = { "Technician", "Sale Representative", "Production Manager" };

    public EmployeeMgmt()
	{
		InitializeComponent();

        this.employeeManagement = new EmployeeManagement();

        List<Employee> employees = employeeManagement.GetAllEmployee();

        employeeListView.ItemsSource = employees;
        jobTitlePicker.ItemsSource = jobTitleList;
    }

    public void OnConfirmClickedEmployee(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameEntry");
        Entry surname = this.FindByName<Entry>("surnameEntry");
        Entry mail = this.FindByName<Entry>("mailEntry");
        Picker jobtitle = this.FindByName<Picker>("jobTitlePicker");
        Entry phone = this.FindByName<Entry>("phoneEntry");

        this.employeeManagement.SendEmployee(jobTitleList,name, surname, mail, jobtitle, phone);

        Navigation.PushAsync(new EmployeeMgmt());
        Navigation.RemovePage(this);

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

