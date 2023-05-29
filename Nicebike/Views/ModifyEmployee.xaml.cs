namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;
using MySql.Data.MySqlClient;


public partial class ModifyEmployee : ContentPage
{
    private EmployeeManagement employeeManagement = new EmployeeManagement();
    private string[] jobTitleList = { "Technician", "Sale Representative", "Production Manager" };
    private int employeeId;

    public ModifyEmployee(Employee employee)
	{
		InitializeComponent();
        employeeId = employee.idEmployee;
        jobTitlePicker.ItemsSource = jobTitleList;
        BindingContext = employee;
    }

    public async void modifyClickedEmployee(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry surname = this.FindByName<Entry>("surnameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Picker jobtitle = this.FindByName<Picker>("jobTitlePicker");
        Entry phone = this.FindByName<Entry>("phoneChange");

        employeeManagement.modifyEmployee(jobTitleList, employeeId, name.Text, surname.Text, mail.Text, jobtitle, phone.Text);

        await Navigation.PushAsync(new EmployeeMgmt());
        Navigation.RemovePage(this);
    }
}