using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicebike.Models
{
    //internal class Bike
    //{
    //    public string Type { get; set; }
    //    public string Color { get; set; }

    //    public double Size { get; set; }
    //}

    public class Bike
    {
        public string color;
        public string Type;
        public int Size;

        public Bike(string color, string Type, int Size)
        {
            this.color = color;
            this.Type = Type;
            this.Size = Size;
        }
    }
}
