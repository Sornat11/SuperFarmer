# SuperFarmer

## Opis
SuperFarmer to gra konsolowa inspirowana popularną grą planszową. Gracze rywalizują, aby jako pierwsi zebrać pełen zestaw zwierząt. Gra zawiera mechanizmy takie jak rzuty kostką, wymiana zwierząt i unikanie ataków drapieżników.

## Zasady Gry
- **Rzuty kostką:** Gracze rzucają dwiema kostkami, aby zdecydować, które zwierzęta mogą zdobyć lub stracić w danej rundzie.
- **Wymiana zwierząt:** Gracze mogą wymieniać zwierzęta  z "głównym stadem" na podstawie zdefiniowanych kursów wymiany.
- **Ataki drapieżników:** Rzuty kostką mogą również skutkować atakami drapieżników, które mogą zmusić graczy do utraty zwierząt, chyba że są chronieni przez psy.
- **Zwycięstwo:** Gra kończy się, gdy któryś z graczy uzbiera wszystkie wymagane zwierzęta.

## Wymagania
- .NET Core 3.1 lub nowszy
- Konsola/terminal do uruchomienia gry

## Instalacja i Uruchomienie
```bash
git clone [url-do-repozytorium]
cd SuperFarmer
dotnet run
```

## Struktura Projektu
- `Game.cs`: Główna klasa gry, zarządza logiką rozgrywki.
- `Player.cs`: Reprezentuje gracza, zarządzanie stanem zwierząt gracza.
- `Animal.cs`: Klasa dla zwierząt, zawiera informacje o typie i ilości.
- `Die.cs`: Reprezentuje kostkę, zarządza rzutami.
- `TradeTable.cs`: Zarządza wymianą zwierząt między graczami.
- `AnimalStock.cs`: Zarządza "głównym stadem" zwierząt dostępnych do wymiany i zdobycia.
- `GameConsole.cs`: Odpowiada za wyświetlanie informacji na konsoli i interakcję z użytkownikiem.

## Autorzy
- Jakub Sornat (jakubsornat2001@gmail.com)

## Wkład
