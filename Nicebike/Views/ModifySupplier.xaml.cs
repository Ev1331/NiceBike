namespace Nicebike.Views;
using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;

public partial class ModifySupplier : ContentPage
{
    private SupplierManagement supplierManagement = new SupplierManagement();
    private int supplierId;

    public ModifySupplier(Supplier supplier)
    {
        InitializeComponent();
        supplierId = supplier.idSupplier;
        BindingContext = supplier;
    }

    public async void modifyClicked(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Entry phone = this.FindByName<Entry>("phoneChange");
        Entry street = this.FindByName<Entry>("streetChange");
        Entry number = this.FindByName<Entry>("numberChange");
        Entry town = this.FindByName<Entry>("townChange");

        supplierManagement.modifySupplier(supplierId, name.Text, mail.Text, phone.Text, street.Text, town.Text, number.Text);

        var supplierPage = new SuppliersManagement();
        await Navigation.PushAsync(supplierPage);

        Navigation.RemovePage(this);
    }
}