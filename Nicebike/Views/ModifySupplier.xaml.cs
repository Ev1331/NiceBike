﻿namespace Nicebike.Views;
using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using Nicebike.ViewModels;



public partial class ModifySupplier : ContentPage
{
    Supplier supplier;
    int supplierId;

    public ModifySupplier(Supplier supplier)
    {
        InitializeComponent();
        this.supplier = supplier;
        supplierId = supplier.idSupplier;

        // Définir le contexte de liaison des données
        BindingContext = supplier;
    }

    public void modifyClicked(object sender, EventArgs e)
    {
        Entry name = this.FindByName<Entry>("nameChange");
        Entry mail = this.FindByName<Entry>("mailChange");
        Entry phone = this.FindByName<Entry>("phoneChange");
        Entry street = this.FindByName<Entry>("streetChange");
        Entry number = this.FindByName<Entry>("numberChange");
        Entry town = this.FindByName<Entry>("townChange");

        ModifyDataSupplier modifyDataSupplier = new ModifyDataSupplier();
        modifyDataSupplier.modifySupplier(supplierId, name.Text, mail.Text, phone.Text, street.Text, town.Text, number.Text);
    }
}


