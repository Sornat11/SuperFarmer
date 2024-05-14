using System;
using System.Collections.Generic;

namespace SuperFarmer
{
    public class Player
    {
        public int PlayerId { get; private set; }
        public Dictionary<string, Animal> Animals { get; private set; }

        public Player(int playerId)
        {
            PlayerId = playerId;
            // Inicjalizacja zwierząt dla gracza
            Animals = new Dictionary<string, Animal>
            {
                {"Królik", new Animal("Królik", 1)},
                {"Owca", new Animal("Owca")},
                {"Świnia", new Animal("Świnia")},
                {"Krowa", new Animal("Krowa")},
                {"Koń", new Animal("Koń")},
                {"Mały Pies", new Animal("Mały Pies")},
                {"Duży Pies", new Animal("Duży Pies")}
            };
        }

        // Dodanie zwierząt do kolekcji gracza
        public void AddAnimal(string type, int quantity)
        {
            if (Animals.ContainsKey(type))
            {
                Animals[type].Quantity += quantity;
            }
        }

        // Usunięcie zwierząt z kolekcji gracza, zwraca true jeśli operacja się udała
        public bool RemoveAnimals(string animal, int amount)
        {
            if (CanTrade(animal, amount))
            {
                Animals[animal].Quantity -= amount;
                return true;
            }
            return false;
        }

        // Wyświetlenie aktualnej kolekcji zwierząt gracza
        public void ShowAnimals()
        {
            Console.WriteLine("\nObecny stan Twoich zwierząt:");
            foreach (var animal in Animals)
            {
                Console.WriteLine($" - {animal.Key}: {animal.Value.Quantity}");
            }
        }

        // Obliczanie liczby zwierząt do dodania na podstawie wyników rzutu kostką
        public int CalculateAnimalsToAdd(string animalType, int diceCount)
        {
            // Zawsze dodaj zwierzęta wynikające z rzutu
            int totalAnimals = Animals[animalType].Quantity + diceCount;

            if (diceCount == 2)
            {
                return 2 + (totalAnimals / 2 - 1); // Dodajemy dwie sztuki za parę minus już zliczona para
            }
            else
            {
                // Liczymy pełne pary zwierząt
                int pairs = totalAnimals / 2;
                return pairs;
            }
        }

        // Sprawdzenie, czy gracz ma wszystkie wymagane zwierzęta
        public bool HasAllRequiredAnimals()
        {
            return Animals["Królik"].Quantity > 0 && Animals["Owca"].Quantity > 0 &&
                   Animals["Świnia"].Quantity > 0 && Animals["Krowa"].Quantity > 0 &&
                   Animals["Koń"].Quantity > 0;
        }

        // Sprawdzenie, czy gracz może handlować danym zwierzęciem w podanej ilości
        public bool CanTrade(string animal, int amount)
        {
            return Animals.ContainsKey(animal) && Animals[animal].Quantity >= amount;
        }
    }
}