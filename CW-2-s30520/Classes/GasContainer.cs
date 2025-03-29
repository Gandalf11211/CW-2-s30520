using CW_2_s30520.Interface;

namespace CW_2_s30520.Classes;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }

    public GasContainer(double maxCapacity, double height, double depth, double weight, double pressure)
        : base("G", maxCapacity, height, depth, weight)
    {
        Pressure = pressure;
    }
    
    public void Notify(string notification, string serialNumber)
    {
        Console.WriteLine($"Podjęto próbę wykonania niebezpiecznej operacji na kontenerze o numerze seryjnym: {serialNumber}");
    }
    
    public override void UnloadCargo()
    {
        double keepCargo = CargoMass * 0.05;
        CargoMass = keepCargo;
    }

    public override string ToString()
    {
        return base.ToString() + $"\n > ciśnienie w atmosferze: {Pressure} hPa";
    }
}