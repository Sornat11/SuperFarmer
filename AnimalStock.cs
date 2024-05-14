using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFarmer
{
    public class AnimalStock
    {
        private Dictionary<string, int> stock;

        public AnimalStock()
        {
            stock = new Dictionary<string, int>
            {
                {"Królik", 60},
                {"Owca", 24},
                {"Świnia", 20},
                {"Krowa", 12},
                {"Koń", 6},
                {"Mały Pies", 4},
                {"Duży Pies", 2}
            };
        }

        // Sprawdzanie czy w magazynie zwierząt jest ich wystarczająca ilość
        public bool TryRemoveAnimals(string type, int quantity)
        {
            if (stock.ContainsKey(type) && stock[type] >= quantity)
            {
                stock[type] -= quantity;
                return true;
            }
            return false;
        }

        // Zwracanie zwierząt do magazynu
        public void AddAnimals(string type, int quantity)
        {
            if (stock.ContainsKey(type))
            {
                stock[type] += quantity;
            }
            else
            {
                stock.Add(type, quantity);
            }
        }

        // Zwracanie dostępnej ilości zwierząt danego gatunku
        public int GetAvailableQuantity(string type)
        {
            return stock.ContainsKey(type) ? stock[type] : 0;
        }

        // Kopia słownika reprezentująca aktualny stan zapasów
        public Dictionary<string, int> GetStockStatus()
        {
            return new Dictionary<string, int>(stock);
        }

    }
}
