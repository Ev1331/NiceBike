namespace Nicebike.Models
{
    public class Customer
    {
        public int idCustomer;
        public string name;
        public string surname;
        public string mail;
        public string phone;
        public string street;
        public string town;
        public string number;

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
