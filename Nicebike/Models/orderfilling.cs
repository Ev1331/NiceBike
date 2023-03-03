using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicebike.Models;
internal class Orderfilling
    {
    public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
    
    
    }

