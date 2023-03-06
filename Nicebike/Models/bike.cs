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
        public string Color { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }

        public Bike() { }

        public Bike(string Color, string Type, string Size)
        {
            this.Color = Color;
            this.Type = Type;
            this.Size = Size;
        }

    }
}
