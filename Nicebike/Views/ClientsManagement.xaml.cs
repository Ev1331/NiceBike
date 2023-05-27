namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class ClientsManagement : ContentPage
{
    CustomersManagement customersManagement;

	public ClientsManagement()
	{
		InitializeComponent();

        this.customersManagement = new CustomersManagement();

        List<Customer> customers = customersManagement.GetAllCustomers();

		customersListView.ItemsSource = customers;

	}

	public void OnConfirmClickedCustomer(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameEntry");
        Entry surname = this.FindByName<Entry>("surnameEntry");
        Entry mail = this.FindByName<Entry>("mailEntry");
        Entry phone = this.FindByName<Entry>("phoneEntry");
        Entry street = this.FindByName<Entry>("streetEntry");
        Entry town = this.FindByName<Entry>("townEntry");
        Entry number = this.FindByName<Entry>("numberEntry");

        this.customersManagement.SendCustomer(name, surname, mail, phone, street, town, number);

        List<Customer> customers = customersManagement.GetAllCustomers();

        customersListView.ItemsSource = customers;

    }

    public void OnDeleteClickedCustomer(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idCustomer = (int)button.CommandParameter;

        this.customersManagement.DeleteCustomer(idCustomer);

        List<Customer> customers = customersManagement.GetAllCustomers();

        customersListView.ItemsSource = customers;

    }

    public async void OnModifyClickedCustomer(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var customer = (Customer)button.BindingContext;


        var modifyPage = new ModifyCustomer(customer);


        await Navigation.PushAsync(modifyPage);

        Navigation.RemovePage(this);

    }
}