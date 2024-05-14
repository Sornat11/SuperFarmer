using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFarmer
{
    public class Animal
    {
        public string Type { get; private set; }
        public int Quantity { get; set; }

        // Konstruktor przyjmujący typ zwierzęcia i opcjonalnie jego początkową ilość
        public Animal(string type, int initialQuantity = 0)
        {
            Type = type;
            Quantity = initialQuantity;
        }
    }
}
