namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;
using MySql.Data.MySqlClient;


public partial class ModifyEmployee : ContentPage
{
    Employee employee;
    int employeeId;
    string[] jobTitleList = { "Technician", "Sale Representative", "Production Manager" };

    public ModifyEmployee(Employee employee)
	{
		InitializeComponent();

        this.employee = employee;
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

        ModifyDataEmployee modifyDataEmployee = new ModifyDataEmployee();
        modifyDataEmployee.modifyEmployee(jobTitleList, employeeId, name.Text, surname.Text, mail.Text, jobtitle, phone.Text);

        await Navigation.PushAsync(new EmployeeMgmt());
        Navigation.RemovePage(this);

    }
}
