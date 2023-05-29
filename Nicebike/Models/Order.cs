namespace Nicebike.Models
{
    public class Order
    {
        public int IdOrder { get; set; }
        public int CustomerId { get; set; }
        public string Date { get; set; }
        public string DeliveryDate { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public Order(int IdOrder, int CustomerId, string Date, string DeliveryDate, string Status, string CustomerName)
        {
            this.IdOrder = IdOrder;
            this.CustomerId = CustomerId;
            this.Date = Date;
            this.DeliveryDate = DeliveryDate;
            this.Status = Status;
            this.CustomerName = CustomerName;
        }
    }
}