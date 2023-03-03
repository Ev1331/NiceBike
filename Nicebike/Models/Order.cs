using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicebike.Models;

//internal class Order
//{
//    public double Id { get; set; }
//    public Customer Customer { get; set; }

//    public List<Bike> Bike { get; set; }
    
//}


public class Order
    {
    public int Id;
    public List<Bike> bikeList;
    public Customer customer;

    public Order(List<Bike> bikeList, Customer customer, int Id ) {
        this.customer = customer;
        this.Id = Id;
        this.bikeList = bikeList;

    }
    }