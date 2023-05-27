namespace Nicebike.Views;
using Nicebike.Models;
using Nicebike.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Reflection.Metadata;

public partial class ModifyPart : ContentPage
{
    Part part;
	int IdPart;
    int i = 0;
    List<String> suppliersnames = new List<String>();
    List<Supplier> suppliers = new List<Supplier>();
    public ModifyPart(Part part)
    {
        InitializeComponent();
        this.part = part;
        IdPart = part.id;

        // Cr�er une instance de la classe SupplierManagement
        SupplierManagement supplierManagement = new SupplierManagement();

        // R�cup�rer la liste des fournisseurs � partir de la base de donn�es
        suppliers = supplierManagement.GetAllSuppliers();

        foreach (Supplier supplier in suppliers)
        {
            suppliersnames.Add(suppliers[i].name);
            i = i + 1;
        }

        // Assigner la liste des fournisseurs � la source de donn�es du ListView
        supplierPickerModify.ItemsSource = suppliersnames;

        // D�finir le contexte de liaison des donn�es
        BindingContext = part;
    }

    public async void modifyClickedPart(object sender, EventArgs e)
    {
        Entry reference = this.FindByName<Entry>("referenceEntryModify");
        Entry description = this.FindByName<Entry>("descriptionEntryModify");
        Entry quantity = this.FindByName<Entry>("quantityEntryModify");
        Entry threshold = this.FindByName<Entry>("thresholdEntryModify");
        Picker supplier = this.FindByName<Picker>("supplierPickerModify");

        ModifyDataPart modifyDataPart = new ModifyDataPart();
        modifyDataPart.ModifyPart(IdPart, suppliers, reference, description, quantity, threshold, supplier);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

}
