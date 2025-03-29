namespace CW_2_s30520.Classes;

public abstract class Container
{
    public double CargoMass { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double Depth { get; set; }
    public string SerialNumber { get; set; }
    public double MaxCapacity { get; set; }
    public string ContainerSymbol { get; set; }
    
    private static Dictionary <string, int> serialNumbers = new Dictionary <string, int> ();

    protected Container(string containerSymbol, double maxCapacity, double height, double depth, double weight)
    {
        ContainerSymbol = containerSymbol;
        MaxCapacity = maxCapacity;
        Height = height;
        Depth = depth;
        Weight = weight;
        SerialNumber = createSerialNumber(containerSymbol);
    }

    private static string createSerialNumber(string containerSymbol)
    {
        if (!serialNumbers.ContainsKey(containerSymbol))
        {
            serialNumbers[containerSymbol] = 0;
        }

        serialNumbers[containerSymbol]++;

        return $"KON-{containerSymbol}-{serialNumbers[containerSymbol]}";
    }

    public virtual void LoadCargo(double mass)
    {
        if (mass > MaxCapacity)
        {
            throw new OverfillException($"""
                                        Nie można załadować ładunku o podanej masie: {mass} kg ! 
                                        Kontener o numerze seryjny: {SerialNumber}, maksymalnie może pomieścić {MaxCapacity} kg. 
                                        """);
        }
        CargoMass = mass;
    }

    public virtual void UnloadCargo()
    {
        CargoMass = 0;
    }

    public override string ToString()
    {
        return $"""
                 Kontener o numerze seryjnym: {SerialNumber} ma: 
                 > masę ładunku: {CargoMass} kg,
                 > wysokość: {Height} cm,
                 > głębokość: {Depth} cm,
                 > maksymalną pojemonść {MaxCapacity} kg
                """;
    }
}