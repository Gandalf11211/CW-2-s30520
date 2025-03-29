using CW_2_s30520.Interface;

namespace CW_2_s30520.Classes;

public class RefrigeratedContainer : Container, IHazardNotifier
{
    public string ItemType { get; set; }
    public double Temperature { get; set; }
    
    public RefrigeratedContainer(double maxCapacity, double height, double depth, double weight)
        : base("C", maxCapacity, height, depth, weight)
    {
        
    }

    private static Dictionary<string, double> itemsData = new Dictionary<string, double>()
    {
        { "Banan", 13.3 },
        { "Czekolada", 18 },
        { "Ryba", 2 },
        { "Mięso", -15 },
        { "Lody", -18 },
        { "Mrożona Pizza", -30},
        { "Ser", 7.2 },
        { "Kiełbasa", 5 },
        { "Masło", 20.5 },
        { "Jajka", 19 }
};
    
    public RefrigeratedContainer(double maxCapacity, double height, double depth, double weight, string itemType,  double temperature)
        : base("C", maxCapacity, height, depth, weight)
    {
        ItemType = itemType;
        Temperature = temperature;

        if (!itemsData.ContainsKey(ItemType))
        {
            throw new ArgumentException("Kontener nie może przechowywać tego typu produktu");
        }
        
        double itemTemperature = itemsData[itemType];

        if (temperature < itemTemperature)
        {
            throw new Exception($"Temperatura kontenera o numerze seyjnym: {SerialNumber} nie może być niższa niż temperatura wymagana przez dany produkt !");
        }
    }
    
    public override string ToString()
    {
        return base.ToString() + $"\n > temperaturę: {Temperature} C \n > produkty rodzaju: {ItemType}";
    }
}