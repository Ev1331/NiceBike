namespace Nicebike;
using MySql.Data.MySqlClient;
using Nicebike.Models;
using System.Collections.ObjectModel;

public partial class StockManagement : ContentPage
{
    public ObservableCollection<Part> okParts = new ObservableCollection<Part>();
    public ObservableCollection<Part> lowParts = new ObservableCollection<Part>();
    public ObservableCollection<Part> observableParts = new ObservableCollection<Part>();
    public PartsManagement stockManagement = new PartsManagement();
    public int IdPart;
    public StockManagement()
    {
        InitializeComponent();

        ObservableCollection<Part> observableParts = stockManagement.GetAllParts();

        foreach (Part part in observableParts){
            if(part.quantity < part.threshold)
            {
                lowParts.Add(part);
            }
            else
            {
                okParts.Add(part);
            }
        }
            partListView.ItemsSource = okParts;
            lowPartListView.ItemsSource = lowParts;
    }

    private async void NewPart(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PartDatasheet());
    }

    private async void OnModifyClickedPart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var part = (Part)button.BindingContext;
        var modifyPage = new ModifyPart(part);

        await Navigation.PushAsync(modifyPage);
    }

    private async void DeletePart(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdPart = (int)button.CommandParameter;

        PartsManagement stockManagement = new PartsManagement();
        stockManagement.DeletePart(IdPart);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

    private async void AddQuantityClick(object sender, EventArgs e)
    {
        Entry quantity = this.FindByName<Entry>("QuantityEntry");
        stockManagement.AddQuantity(IdPart, quantity);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
    }

    private async void RemoveQuantityClick(object sender, EventArgs e)
    {
        Entry quantity = this.FindByName<Entry>("QuantityEntry");
        stockManagement.RemoveQuantity(IdPart, quantity);

        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);

    }

    private void partSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        //searchResults.ItemsSource = customersList.GetSearchResults(searchBar.Text);
        partSearchResults.ItemsSource = stockManagement.GetAllParts();
    }

    private void partSearchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        IdPart = ((Part)(partSearchResults.SelectedItem)).id;
    }

    private async void RestockAllClick(object sender, EventArgs e)
    {
        if (lowParts.Count != 0)
        {
        stockManagement.RestockAll(lowParts);
        await Navigation.PushAsync(new StockManagement());
        Navigation.RemovePage(this);
        }
    }
}

public class PartsManagement
{
    MySqlConnection connection = new MySqlConnection("server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;");
    string sql;
    public ObservableCollection<Part> GetAllParts()
    {
        int idSupplier;
        SupplierManagement supplierManagement = new SupplierManagement();
        List<Supplier> suppliers = new List<Supplier>();
        suppliers = supplierManagement.GetAllSuppliers();

        ObservableCollection<Part> parts = new ObservableCollection<Part>();

        connection.Open();

        sql = "SELECT * FROM dbNicebike.part";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            idSupplier = reader.GetInt32("Supplier");
            Part part = new Part(
                reader.GetInt32("IdPart"),
                reader.GetString("Ref"),
                reader.GetString("Description"),
                reader.GetInt32("Quantity"),
                reader.GetInt32("Threshold"),
                idSupplier,
                suppliers.Find(obj => obj.idSupplier == idSupplier).name
            );
            parts.Add(part);
        }
        connection.Close();
        return parts;
    }

    public void SendPart(List <Supplier> suppliers, Entry reference, Entry description, Entry quantity, Entry threshold, Picker supplier)
    {
        connection.Open();

        sql = "INSERT INTO dbNicebike.part (Ref, Description, Quantity, Threshold, Supplier) VALUES (@reference, @description, @quantity, @threshold, @supplier)";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@reference", reference.Text);
        command.Parameters.AddWithValue("@description", description.Text);
        command.Parameters.AddWithValue("@quantity", quantity.Text);
        command.Parameters.AddWithValue("@threshold", threshold.Text);
        command.Parameters.AddWithValue("@supplier", suppliers[supplier.SelectedIndex].idSupplier);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public void DeletePart(int IdPart)
    {
        connection.Open();
        
        sql = "DELETE FROM dbNicebike.part WHERE idPart = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", IdPart);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public void AddQuantity(int IdPart, Entry quantity)
    {
        connection.Open();
        sql = "UPDATE dbNicebike.part SET Quantity = Quantity + @AddedQuantity WHERE IdPart = @IdPart";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdPart", IdPart);
        command.Parameters.AddWithValue("@AddedQuantity", quantity.Text);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public void RemoveQuantity(int IdPart, Entry quantity)
    {
        connection.Open();
        sql = "UPDATE dbNicebike.part SET Quantity = Quantity - @AddedQuantity WHERE IdPart = @IdPart";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdPart", IdPart);
        command.Parameters.AddWithValue("@AddedQuantity", quantity.Text);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public async void RestockAll(ObservableCollection<Part> lowParts)
    {
        connection.Open();

        foreach (Part part in lowParts)
        {
        sql = "UPDATE dbNicebike.part SET Quantity = Quantity + @threshold WHERE IdPart = @IdPart";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdPart", part.id);
        command.Parameters.AddWithValue("@threshold", part.threshold);
        command.ExecuteNonQuery();
        }
        connection.Close();
    }
}