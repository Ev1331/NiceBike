namespace Nicebike.Models;
public class OrderDetails
{
    public int IdOrderDetails { get; set; }
    public int Order { get; set; }
    public int Bike { get; set; }

    public OrderDetails(int IdOrderDetails, int Order, int Bike)
    {
        this.IdOrderDetails = IdOrderDetails;
        this.Order = Order;
        this.Bike = Bike;
    }
}
