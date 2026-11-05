using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping
{
    internal class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Name} (осталось: {Quantity})";
        }
            
            
    }
}
