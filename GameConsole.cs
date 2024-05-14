using System;
using System.Collections.Generic;

namespace SuperFarmer
{
    public static class GameConsole
    {
        // Wyświetlenie powitania
        public static void ShowWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Witaj w grze Superfarmer! ===");
            Console.ResetColor();
        }

        // Pobranie liczby graczy
        public static int GetPlayerCount()
        {
            int playerCount;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nIlu graczy będzie uczestniczyć w grze? (2-4)");
            Console.ResetColor();

            // Pętla do momentu, aż użytkownik poda prawidłową liczbę graczy
            while (!int.TryParse(Console.ReadLine(), out playerCount) || playerCount < 2 || playerCount > 4)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Nieprawidłowa liczba graczy. Proszę podać liczbę graczy od 2 do 4:");
                Console.ResetColor();
            }

            return playerCount;
        }

        // Wyświetlenie nagłówka rundy
        public static void ShowRoundHeader(int round)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n╔==============================╗");
            Console.WriteLine($"║=== Runda {round} ===║");
            Console.WriteLine($"╚==============================╝");
            Console.ResetColor();
        }

        // Wyświetlenie aktualnego stanu magazynu zwierząt
        public static void ShowCurrentAnimalStock(AnimalStock stock)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔===============================================╗");
            Console.WriteLine("║ Aktualny stan dostępnych zwierząt w magazynie ║");
            Console.WriteLine("╚===============================================╝");

            foreach (var animal in stock.GetStockStatus())
            {
                Console.WriteLine($" - {animal.Key}: {animal.Value}");
            }

            Console.ResetColor();
        }

        // Wyświetlenie stanu zwierząt gracza
        public static void ShowPlayerAnimals(Player player)
        {
            // Kolorowanie w zależności od identyfikatora gracza
            ConsoleColor playerColor = player.PlayerId % 2 == 0 ? ConsoleColor.Magenta : ConsoleColor.Green;
            Console.ForegroundColor = playerColor;
            Console.WriteLine($"\n--- Runda gracza {player.PlayerId} ---");
            Console.WriteLine("\nObecny stan Twoich zwierząt:");

            foreach (var animal in player.Animals)
            {
                Console.WriteLine($" - {animal.Key}: {animal.Value.Quantity}");
            }

            Console.ResetColor();
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby rozpocząć ruch...");
            Console.ReadKey();
        }

        // Wyświetlenie informacji o ataku drapieżnika
        public static void ShowPredatorAttack(string predator)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nUwaga! {predator} zaatakował!");
            Console.ResetColor();
        }
    }
}