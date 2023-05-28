namespace Nicebike.Views;
using Nicebike.Models;
using MySql.Data.MySqlClient;
using Nicebike.ViewModels;

public partial class ModifyCustomer : ContentPage
{
    Customer customer;
    int customerId;
    CustomersManagement customersManagement = new CustomersManagement();

	public ModifyCustomer(Customer customer)
	{
		InitializeComponent();
        this.customer = customer;
        customerId = customer.idCustomer;

        BindingContext = customer;
	}


    public async void modifyClickedCustomer(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry surname = this.FindByName<Entry>("surnameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Entry phone = this.FindByName<Entry>("phoneChange");
        Entry street = this.FindByName<Entry>("streetChange");
        Entry town = this.FindByName<Entry>("townChange");
        Entry number = this.FindByName<Entry>("numberChange");

        customersManagement.modifyCustomer(customerId, name.Text, surname.Text, mail.Text, phone.Text, street.Text, town.Text, number.Text);

        await Navigation.PushAsync(new ClientsManagement());

        Navigation.RemovePage(this);


    }
}



