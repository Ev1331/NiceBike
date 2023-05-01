namespace Nicebike.Models
{
    public class Part
    {
        public int id { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public int threshold { get; set; }
        public int supplier { get; set; }

        public Part(int id, string reference, string description, int quantity, int threshold, int supplier)
        {
            this.id = id;
            this.reference = reference;
            this.description = description;
            this.quantity = quantity;
            this.threshold = threshold;
            this.supplier = supplier;

        }
    }
}
