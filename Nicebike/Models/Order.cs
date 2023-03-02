using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicebike.Models;

internal class Order
{
    public double Id { get; set; }
    public Customer Customer { get; set; }

    public ObservableCollection<Bike> Bike { get; set; }
    
}
