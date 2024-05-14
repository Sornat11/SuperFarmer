using System;
using System.Collections.Generic;

namespace SuperFarmer
{
    public class TradeTable
    {
        public Dictionary<string, Dictionary<string, double>> Rates { get; private set; }

        // Inicjuje nową instancję tabeli handlowej, która zawiera stawki wymiany pomiędzy różnymi typami zwierząt.
        // Stawki są wyrażone jako iloraz jednostek jednego typu zwierzęcia do jednostek innego typu.
        public TradeTable()
        {
            Rates = new Dictionary<string, Dictionary<string, double>>
            {
                // Inicjalizacja stawek wymiany dla poszczególnych typów zwierząt.
                // Stawki są wyrażone jako iloraz jednostek jednego typu zwierzęcia do jednostek innego typu.
                // Na przykład: 1 królik = 6 owiec.
                {"Królik", new Dictionary<string, double>
                    {
                        {"Owca", 6},
                        {"Świnia", 12},
                        {"Krowa", 36},
                        {"Koń", 72},
                        {"Mały Pies", 6},
                        {"Duży Pies", 36}
                    }
                },
                {"Owca", new Dictionary<string, double>
                    {
                        {"Królik", 1.0 / 6},
                        {"Świnia", 2},
                        {"Krowa", 6},
                        {"Koń", 12},
                        {"Mały Pies", 1},
                        {"Duży Pies", 6}
                    }
                },
                {"Świnia", new Dictionary<string, double>
                    {
                        {"Królik", 1.0 / 12},
                        {"Owca", 0.5},
                        {"Krowa", 3},
                        {"Koń", 6},
                        {"Mały Pies", 0.5},
                        {"Duży Pies", 3}
                    }
                },
                {"Krowa", new Dictionary<string, double>
                    {
                        {"Królik", 1.0 / 36},
                        {"Owca", 1.0 / 6},
                        {"Świnia", 1.0 / 3},
                        {"Koń", 2},
                        {"Mały Pies", 1.0 / 6},
                        {"Duży Pies", 1}
                    }
                },
                {"Koń", new Dictionary<string, double>
                    {
                        {"Królik", 1.0 / 72},
                        {"Owca", 1.0 / 12},
                        {"Świnia", 1.0 / 6},
                        {"Krowa", 0.5},
                        {"Mały Pies", 1.0 / 12},
                        {"Duży Pies", 0.5}
                    }
                },
                {"Mały Pies", new Dictionary<string, double>
                    {
                        {"Królik", 1.0 / 6},
                        {"Owca", 1},
                        {"Świnia", 2},
                        {"Krowa", 6},
                        {"Koń", 12},
                        {"Duży Pies", 6}
                    }
                },
                {"Duży Pies", new Dictionary<string, double>
                    {
                        {"Królik", 1.0 / 36},
                        {"Owca", 1.0 / 6},
                        {"Świnia", 1.0 / 3},
                        {"Krowa", 1},
                        {"Koń", 2},
                        {"Mały Pies", 1.0 / 6}
                    }
                }
            };
        }

        // Sprawdza, czy wymiana jest możliwa przy podanej ilości zwierząt
        public bool CanTrade(string from, string to, double amount)
        {
            return Rates.ContainsKey(from) && Rates[from].ContainsKey(to) && amount >= Rates[from][to];
        }

        // Oblicza ilość zwierząt, które można otrzymać w wyniku wymiany
        public double CalculateTrade(string from, string to, double amount)
        {
            if (CanTrade(from, to, amount))
            {
                return amount / Rates[from][to];
            }
            return 0;
        }
    }
}