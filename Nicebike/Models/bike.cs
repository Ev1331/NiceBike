namespace Nicebike.Models
{
    public class Bike
    {
        public int id { get; set; }
        public string color { get; set; }
        public string type { get; set; }
        public string size { get; set; }
        public string reference { get; set; }
        public int technician { get; set; }
        public int bikeModel { get; set; }
        public string status { get; set; }

        public Bike(int id, string color, string type, string size, string reference, int technician, int bikeModel, string status)
        {
            this.id = id;
            this.color = color;
            this.type = type;
            this.size = size;
            this.reference = reference;
            this.technician = technician;
            this.bikeModel = bikeModel;
            this.status = status;
        }
    }
}