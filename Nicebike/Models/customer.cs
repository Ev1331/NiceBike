using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicebike.Models
{
    //class Customer
    //{
    //    public string Name { get; set; }

    //    public string Mail { get; set; }

    //    public string Phone { get; set; }

    //    public string Street { get; set; }

    //    public string Town { get; set; }

    //    public string Number { get; set; }
    //}

    public class Customer
    {
        public string name;
        public string mail;
        public string phone;
        public string street;
        public string town;
        public string number;

        public Customer(string name, string mail, string phone, string street, string town, string number ) {
        this.name = name;
        this.mail = mail;
        this.phone = phone;
        this.street = street;
        this.town = town;
        this.number = number;}
    }
}
