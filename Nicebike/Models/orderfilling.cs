using System.Collections.ObjectModel;

namespace Nicebike.Models;
internal class Orderfilling
    {
    public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
    
    
    }

