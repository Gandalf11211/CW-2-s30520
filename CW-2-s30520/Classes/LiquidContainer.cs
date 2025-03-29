using CW_2_s30520.Interface;

namespace CW_2_s30520.Classes;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(double maxCapacity, double height, double depth, double weight)
    : base("L", maxCapacity, height, depth, weight)
    {
        
    }

    public void Notify(string notification, string serialNumber)
    {
        Console.WriteLine($"Podjęto próbę wykonania niebezpiecznej operacji na kontenerze o numerze seryjnym: {serialNumber}");
    }

    public void LoadCargo(double mass, bool isHazardous)
    {
       double capacity =  isHazardous ? MaxCapacity * 0.5 : MaxCapacity * 0.9;

       if (mass > capacity)
       {
           string? information = null;
           
           if (isHazardous)
           {
               information = "Kontener przechowujący niebezpieczny ładunek, może być załadowany tylko do 50% jego pojemności";
           }
           else
           {
               information = "Kontener przechowujący zwykły ładunek, może być załadowany tylko do 90% jego pojemności";
           }
           
           Notify(information, SerialNumber);
       }

       if (capacity > MaxCapacity)
       {
           try
           {
               LoadCargo(mass);
           }
           catch (OverfillException e)
           {
               Console.WriteLine(e.Message);
           }
       }
        
    }
}