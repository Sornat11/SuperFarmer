using System;
using System.Collections.Generic;

namespace SuperFarmer
{
    public class Game
    {
        private List<Player> Players;
        private Die Die1, Die2;
        private AnimalStock animalStock;
        private TradeTable tradeTable;

        public Game(int numberOfPlayers)
        {
            Players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player(i + 1));
            }

            InitializeDice();
            animalStock = new AnimalStock();
            tradeTable = new TradeTable();
        }

        private void InitializeDice()
        {
            Die1 = Die.CreateDie1();
            Die2 = Die.CreateDie2();
        }

        public void StartGame()
        {
            int round = 1;  // Zaczynamy od rundy 1
            while (!CheckVictory())
            {
                GameConsole.ShowRoundHeader(round);
                GameConsole.ShowCurrentAnimalStock(animalStock);

                foreach (var player in Players)
                {
                    GameConsole.ShowPlayerAnimals(player);
                    ConfirmAndPlayRound(player);
                }

                // Monit o kontynuowanie do następnej rundy
                Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować do następnej tury...");
                Console.ReadKey();

                round++;
            }

            Console.WriteLine("\n=== Gra zakończona! ===");
        }


        private void ConfirmAndPlayRound(Player player)
        {
            PerformTrade(player);
            PlayRound(player);
        }

        private void PlayRound(Player player)
        {
            string result1 = Die1.Roll();
            string result2 = Die2.Roll();
            Console.WriteLine($"Wynik rzutu dla gracza: {result1}, {result2}");

            // Obsługa ataku drapieżników
            bool predatorAttack1 = HandlePredators(player, result1);
            bool predatorAttack2 = HandlePredators(player, result2);

            // Jeśli nie było ataku drapieżników, rozmnażamy zwierzęta
            if (!predatorAttack1 && !predatorAttack2)
            {
                DistributeAnimalsFromDiceResults(player, result1, result2);
                Console.WriteLine("\nStan zwierząt po rzucie:");
                player.ShowAnimals();
            }
        }

        private void DistributeAnimalsFromDiceResults(Player player, string result1, string result2)
        {
            int diceCount1 = result1 == result2 ? 2 : 1;
            int diceCount2 = result1 == result2 ? 0 : 1;

            List<string> predators = new List<string> { "Wilk", "Lis" };
            if (predators.Contains(result1) || predators.Contains(result2))
            {
                return; // Nie rozmnażaj drapieżników
            }

            // Dodawanie zwierząt w oparciu o wyniki rzutu kostką
            int animalsToAdd1 = player.CalculateAnimalsToAdd(result1, diceCount1);
            if (animalsToAdd1 > 0)
            {
                if (animalStock.TryRemoveAnimals(result1, animalsToAdd1))
                {
                    player.AddAnimal(result1, animalsToAdd1);
                    Console.WriteLine($"Dodano {animalsToAdd1} sztuk {result1} do kolekcji gracza.");
                }
                else
                {
                    Console.WriteLine($"Brak wystarczającej liczby {result1} w magazynie.");
                }
            }
            else
            {
                Console.WriteLine($"Nie można rozmnożyć {result1}, brak wystarczającej ilości zwierząt.");
            }

            // Dodawanie zwierząt, jeśli wyniki rzutów były różne
            if (!result1.Equals(result2))
            {
                int animalsToAdd2 = player.CalculateAnimalsToAdd(result2, diceCount2);
                if (animalsToAdd2 > 0)
                {
                    if (animalStock.TryRemoveAnimals(result2, animalsToAdd2))
                    {
                        player.AddAnimal(result2, animalsToAdd2);
                        Console.WriteLine($"Dodano {animalsToAdd2} sztuk {result2} do kolekcji gracza.");
                    }
                    else
                    {
                        Console.WriteLine($"Brak wystarczającej liczby {result2} w magazynie.");
                    }
                }
                else
                {
                    Console.WriteLine($"Nie można rozmnożyć {result2}, brak wystarczającej ilości zwierząt.");
                }
            }
        }

        private void PerformTrade(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nCzy chcesz dokonać wymiany? (tak/nie)");
                string response = Console.ReadLine()?.Trim().ToLower();

                if (response != "tak")
                {
                    break;
                }

                Console.WriteLine("Podaj nazwę zwierzęcia, które chcesz oddać:");
                string fromAnimal = Console.ReadLine()?.Trim();
                if (String.IsNullOrEmpty(fromAnimal) || !player.Animals.ContainsKey(fromAnimal))
                {
                    Console.WriteLine("Nieprawidłowe zwierzę. Spróbuj ponownie.");
                    continue;
                }

                Console.WriteLine("Podaj nazwę zwierzęcia, które chcesz otrzymać:");
                string toAnimal = Console.ReadLine()?.Trim();
                if (String.IsNullOrEmpty(toAnimal) || !animalStock.GetStockStatus().ContainsKey(toAnimal))
                {
                    Console.WriteLine("Nieprawidłowe zwierzę. Spróbuj ponownie.");
                    continue;
                }

                Console.WriteLine("Podaj ilość zwierząt, które chcesz oddać:");
                if (!int.TryParse(Console.ReadLine(), out int amountFrom) || amountFrom <= 0)
                {
                    Console.WriteLine("Nieprawidłowa liczba. Wprowadź liczbę większą od zera.");
                    continue;
                }

                double amountTo = tradeTable.CalculateTrade(fromAnimal, toAnimal, amountFrom);
                if (amountTo == 0)
                {
                    Console.WriteLine("Nie można dokonać wymiany na podstawie tabeli wymian.");
                    continue;
                }
                if (!player.CanTrade(fromAnimal, amountFrom))
                {
                    Console.WriteLine("Nie masz wystarczającej liczby zwierząt do wymiany.");
                    continue;
                }

                // Sprawdzenie i dokonanie wymiany zwierząt
                if (animalStock.TryRemoveAnimals(toAnimal, (int)Math.Floor(amountTo)) && player.RemoveAnimals(fromAnimal, amountFrom))
                {
                    player.AddAnimal(toAnimal, (int)Math.Floor(amountTo));
                    animalStock.AddAnimals(fromAnimal, amountFrom);
                    Console.WriteLine("Wymiana zakończona sukcesem.\n");
                }
                else
                {
                    Console.WriteLine("Wymiana nieudana, brak dostępnych zwierząt w magazynie.");

                    animalStock.AddAnimals(toAnimal, (int)Math.Ceiling(amountTo));
                    player.AddAnimal(fromAnimal, amountFrom);
                }

                player.ShowAnimals();
            }
        }

        private bool HandlePredators(Player player, string result)
        {
            if (result == "Lis")
            {
                GameConsole.ShowPredatorAttack(result);
                if (player.Animals.ContainsKey("Mały Pies") && player.Animals["Mały Pies"].Quantity > 0)
                {
                    Console.WriteLine("Mały pies chroni twoje króliki przed lisem. Mały pies zostaje usunięty.");
                    player.RemoveAnimals("Mały Pies", 1);
                    return true; // Drapieżnik zaatakował
                }
                else
                {
                    int rabbitsCount = player.Animals.ContainsKey("Królik") ? player.Animals["Królik"].Quantity : 0;
                    if (rabbitsCount > 1)
                    {
                        Console.WriteLine($"Lis atakuje! Tracisz {rabbitsCount - 1} królików, zostaje ci jeden.");
                        player.Animals["Królik"].Quantity = 1;
                        return true; // Drapieżnik zaatakował
                    }
                }
                return false; // Brak ataku drapieżnika
            }
            else if (result == "Wilk")
            {
                GameConsole.ShowPredatorAttack(result);
                if (player.Animals.ContainsKey("Duży Pies") && player.Animals["Duży Pies"].Quantity > 0)
                {
                    Console.WriteLine("Duży pies chroni twoje zwierzęta przed wilkiem. Duży pies zostaje usunięty.");
                    player.RemoveAnimals("Duży Pies", 1);
                    return true; // Drapieżnik zaatakował
                }
                else
                {
                    Console.WriteLine("Wilk atakuje! Tracisz wszystkie zwierzęta oprócz królików, koni i małego psa.");
                    List<string> animalsToRemove = new List<string> { "Owca", "Świnia", "Krowa" };
                    foreach (var animal in animalsToRemove)
                    {
                        if (player.Animals.ContainsKey(animal))
                        {
                            player.Animals[animal].Quantity = 0;
                        }
                    }
                    return true; // Drapieżnik zaatakował
                }
            }
            return false; // Brak ataku drapieżnika
        }

        private bool CheckVictory()
        {
            foreach (var player in Players)
            {
                if (player.HasAllRequiredAnimals())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"=== Gracz {player.PlayerId} zwyciężył! ===");
                    Console.ResetColor();
                    return true;
                }
            }
            return false;
        }
    }
}