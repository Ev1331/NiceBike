namespace Nicebike;

using Nicebike.Models;
using Nicebike.Views;
using Nicebike.ViewModels;

public partial class OrderFilling : ContentPage
{
    //ListView filling
    string[] colorList = { "Red", "Blue", "Grey"};
    List<String> modelList = new List<String>();
    string[] sizeList = { "26\"", "28\""};

    List<BikeModel> bikeModels = new List<BikeModel>();
    List<Order> orders = new List<Order>();
    int i=0;
    public int IdOrder;

    CustomersManagement customersManagement = new CustomersManagement();
    BikesManagement bikesManagement = new BikesManagement();
    BikeModelsManagement bikeModelsManagement = new BikeModelsManagement();
    OrderManagement orderManagement = new OrderManagement();
    OrderDetailsManagement orderDetailsManagement = new OrderDetailsManagement();

    public OrderFilling(int Id)
    {
        IdOrder = Id; // IdOrder accessible for the SaveBike function, do not remove (!)
        List<Customer> customers = new List<Customer>();
        List<Bike> orderBikes = orderDetailsManagement.GetOrderBikes(IdOrder);
        int IdCustomer;
        int totalPrice;

        InitializeComponent();

        bikeModels = bikeModelsManagement.GetAllBikeModels();
        foreach (BikeModel bikeModel in bikeModels)
        {
            modelList.Add(bikeModels[i].description);
            i++;
        }

        totalPrice = orderDetailsManagement.GetOrderPrice(orderBikes);
        totalPriceLabel.Text = totalPrice.ToString();

        orderDetailsListView.ItemsSource = orderBikes; //List of the bikes from this order
        colorPicker.ItemsSource = colorList;
        modelPicker.ItemsSource = modelList;
        sizePicker.ItemsSource = sizeList;

        orders = orderManagement.GetAllOrders();
        IdCustomer = (orders.Find(obj => obj.IdOrder == Id)).CustomerId;
        customers = customersManagement.GetAllCustomers();
        BindingContext = customers.Find(obj => obj.idCustomer == IdCustomer);
    }

    private async void RemoveBike(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var IdBike = (int)button.CommandParameter;
        bikesManagement.DeleteBike(IdBike);

        await Navigation.PushAsync(new OrderFilling(IdOrder));
        Navigation.RemovePage(this);
    }

    public async void SaveBike(object sender, EventArgs e)
    {

        Picker color = this.FindByName<Picker>("colorPicker");
        Picker size = this.FindByName<Picker>("sizePicker");
        Picker model = this.FindByName<Picker>("modelPicker");
        Entry quantity = this.FindByName<Entry>("quantityEntry");

        int number;
        bool success = int.TryParse(quantity.Text, out number);

        if (success)
        {
            for (int i = 0; i < number; i++)
            {
                bikesManagement.SendBike(colorList, sizeList, bikeModels, color, "...TYPE...", size, "...REF...", model, "Waiting", IdOrder);
            }
        }

        await Navigation.PushAsync(new OrderFilling(IdOrder));
        Navigation.RemovePage(this);
    }

    private void GoToCustomersManagementClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClientsManagement());
    }

    private async void Confirm(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OrderList());
        Navigation.RemovePage(this);
    }
}
