using SuperFarmer;
using System;

class Program
{
    static void Main(string[] args)
    {
        GameConsole.ShowWelcome(); 
        int playerCount = GameConsole.GetPlayerCount(); 

        do
        {
            Game game = new Game(playerCount);
            game.StartGame();

            Console.WriteLine("\nCzy chcesz zagrać ponownie? (tak/nie)");
        } while (Console.ReadLine().Trim().ToLower() == "tak");

        Console.WriteLine("Dziękuję za grę w Superfarmer!");
    }
}