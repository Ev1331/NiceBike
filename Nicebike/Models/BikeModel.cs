namespace Nicebike.Models
{
    public class BikeModel
    {
        public int id { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public BikeModel(int id, int price, string description)
        {
            this.id = id;
            this.price= price;
            this.description = description;
        }
    }
}