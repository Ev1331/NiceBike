namespace Nicebike.Models
{
    public class Customer
    {
        public int idCustomer { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string number { get; set; }
        public Customer(int idCustomer, string name, string surname, string mail, string phone, string street, string town, string number ) {
            this.idCustomer = idCustomer;
            this.name = name;
            this.surname = surname;
            this.mail = mail;
            this.phone = phone;
            this.street = street;
            this.town = town;
            this.number = number;
        }
    }
}
