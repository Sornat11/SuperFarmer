using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFarmer
{
    public class Die
    {
        private List<string> Sides;
        private Random random = new Random();

        public Die(List<string> sides)
        {
            Sides = new List<string>(sides);
        }

        // rzut kostką 
        public string Roll()
        {
            int index = random.Next(Sides.Count);
            return Sides[index];
        }

        // Tworzenie pierwszej dwunastościennej kostki
        public static Die CreateDie1()
        {
            var die1Sides = new List<string> { "Królik", "Królik", "Królik", "Królik", "Królik", "Królik", "Owca", "Owca", "Owca", "Świnia", "Krowa", "Wilk" };
            return new Die(die1Sides);
        }

        // Tworzenie drugiej dwunastościennej kostki
        public static Die CreateDie2()
        {
            var die2Sides = new List<string> { "Królik", "Królik", "Królik", "Królik", "Królik", "Królik", "Owca", "Owca", "Świnia", "Świnia", "Koń", "Lis" };
            return new Die(die2Sides);
        }
    }
}
