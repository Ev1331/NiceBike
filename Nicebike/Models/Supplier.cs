namespace Nicebike.Models
{
	public class Supplier
	{
        public int idSupplier { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string number { get; set; }
        public Supplier(int idSupplier, string name, string mail, string phone, string street, string town, string number)
		{
            this.idSupplier = idSupplier;
            this.name = name;
            this.mail = mail;
            this.phone = phone;
            this.street = street;
            this.town = town;
            this.number = number;
        }
	}
}