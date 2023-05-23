namespace Nicebike.Models
{
    public class Part
    {
        public int id { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public int threshold { get; set; }
        public int supplierId { get; set; }
        public string supplierName { get; set; }

        public Part(int id, string reference, string description, int quantity, int threshold, int supplierId, string supplierName)
        {
            this.id = id;
            this.reference = reference;
            this.description = description;
            this.quantity = quantity;
            this.threshold = threshold;
            this.supplierId = supplierId;
            this.supplierName = supplierName;
        }
    }
}
