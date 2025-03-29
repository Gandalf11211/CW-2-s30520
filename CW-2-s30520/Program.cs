using CW_2_s30520.Classes;

namespace CW_2_s30520;

class Program
{
    static void Main(string[] args)
    {
        /* Tworzenie kontenera na płyny:
         * max pojemność: 20000000 kg,
         * wysokość: 2000 cm
         * głębokość: 2000 cm,
         * waga własna:  10000000 kg
        */
        
        LiquidContainer fosilContainer = new LiquidContainer(20000000, 2000, 2000, 10000000);
        
        /* Tworzenie kontenera na gaz:
         * max pojemność: 15000000 kg,
         * wysokość: 2000 cm
         * głębokość: 2000 cm,
         * waga własna:  10000000 kg
         * ciśnienie w atmosferze: 1013,25 hPa
         */
        
        GasContainer gasContainer = new GasContainer(15000000, 2000, 2000, 10000000, 1013.25);
        
        /* Tworzenie kontenera na chłodniczego:
         * max pojemność: 17000000 kg,
         * wysokość: 2000 cm
         * głębokość: 2000 cm,
         * waga własna:  10000000 kg
         * rodzaj produktu: Mięso
         * temperatura utrzymywana w konterze: -18 C
         */

        //Próba utworzenia obiektu meatContainer wywoła wyjątek, bo temperatura kontenerowca przewożącego mięso musi być >= -15 C
        
        try
        {
            RefrigeratedContainer meatContainer = new RefrigeratedContainer(15000000, 2000, 2000, 10000000, "Mięso", -18);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //Próba utworzenia obiektu cheeseContainer nie spowoduje wyjątku, bo temperatura kontenerowca przewożącego ser jest  >= 7.2 C 
        RefrigeratedContainer cheeseContainer = new RefrigeratedContainer(15000000, 2000, 2000, 10000000, "Ser", 8);
        cheeseContainer.LoadCargo(1000000);
        
        
        // Załadowanie ładunku niebezpiecznego, przekraczając 50% pojemności kontenera fosilContainer wyświetli komunikat na konsoli o  próbie wykonania niebezpiecznej operacji
        
        try
        {
            fosilContainer.LoadCargo(15000000, true);
            fosilContainer.LoadCargo(5000, true);
            
            gasContainer.LoadCargo(1000000);
        }
        catch (OverfillException overfillException)
        {
            Console.WriteLine(overfillException.Message);
        }
        
        //Załadowanie kontenerów na statek - kontenerowiec
        //Próba załadowania kontenerowca większą ilością kontenerów, niż może on pomieścić zakończy się błędem
        
        ContainerShip statek1 = new ContainerShip(35, 1, 20000000000);
        try
        {
            statek1.AddContainer(fosilContainer);
            statek1.AddContainer(gasContainer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        LiquidContainer juiceContainer = new LiquidContainer(20000000, 2000, 2000, 10000000);
        LiquidContainer waterContainer = new LiquidContainer(18000000, 1500, 1900, 11000000);
        LiquidContainer oilContainer = new LiquidContainer(125000000, 1500, 1900, 12000000);

        try
        {
            juiceContainer.LoadCargo(115000);
            waterContainer.LoadCargo(112564);
            oilContainer.LoadCargo(2560452);
        }
        catch (OverfillException overfillException)
        {
            Console.WriteLine(overfillException.Message);
        }

        List<Container> containers = new List<Container>();
        containers.Add(juiceContainer);
        containers.Add(cheeseContainer);
        containers.Add(waterContainer);
        
        //Załadowanie listy kontenerów na statek,
        //Jeśli liczba kontenerów obecnych naliście przekroczyłaby maksymalną liczbę kontenerów, jaką może pomieścić kontenerowiec, zostałby wywołany wykątek
        
        ContainerShip statek2 = new ContainerShip(35, 5, 20000000000);
        try
        {
            statek2.AddContainers(containers);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        //Usunięcie kontenera ze statku2 o nazwie juiceContainer, powiedzie się, gdyż jest obecny na statku2
        //Usunięcie kontera o nazwie fosilContainer ze statku2 zakończy się błędem, gdyż nie jest obecny na statek2
        
        try
        {
            statek2.RemoveContainer(juiceContainer.SerialNumber);
            statek2.RemoveContainer(fosilContainer.SerialNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        //Rozładowanie kontenera o nazwie fosilContainer
        fosilContainer.UnloadCargo();
        
        Console.WriteLine();
        Console.WriteLine("Dane statku2 przed przeniesieniem kontenera o nazwie fosilContainer ze statku1 na statek2");
        statek2.ShowShipDetails();
        
        //Przeniesienie ze statku1 kontenera o nazwie fosilContainer na statek2 
        statek1.TransferContainer(fosilContainer.SerialNumber, statek2);
        
        Console.WriteLine();
        Console.WriteLine("Dane statku2 po przeniesieniu kontenera o nazwie fosilContainer ze statku1 na statek2:");
        statek2.ShowShipDetails();
        
        //Zastąpienie kontenera o nazwie waterContainer o nazwie oilContainer
        try
        {
            statek2.ReplaceContainer(waterContainer.SerialNumber, oilContainer);
        }
        catch (Exception e)
        {
           Console.WriteLine(e.Message);
        }
        
        
        //Wypisanie informacji o danym kontenerze
        Console.WriteLine();
        Console.WriteLine("Wypisanie danych o kontenerze oilContainer:");
        statek2.ShowContainerDetails(oilContainer.SerialNumber);
    }
}