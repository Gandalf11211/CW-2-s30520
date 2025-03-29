namespace CW_2_s30520.Classes;

public class ContainerShip
{
    public List<Container> Containers { get; set; } = new List<Container>();
    
    public double MaxSpeed { get; set; }
    public int MaxContainersAmount { get; set; }
    public double MaxTotalWeight { get; set; }

    public ContainerShip(double maxSpeed, int maxContainersAmount, double maxTotalWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainersAmount = maxContainersAmount;
        MaxTotalWeight = maxTotalWeight;
    }

    public double CalculateTotalWeight()
    {
        return Containers.Sum(c => c.Weight + c.CargoMass);
    }

    public void AddContainer(Container container)
    {
        if (Containers.Count > MaxContainersAmount)
        {
            throw new Exception("Kontenerowiec, nie pomieści więcej kontenerów");
        } 
        
        double currentWeight = CalculateTotalWeight() + container.Weight + container.CargoMass;

        if (currentWeight > MaxTotalWeight)
        {
            throw new Exception("Przekroczono maksymalną wagę kontenerów jaką może przewozić kontenerowiec");
        }
        
        Containers.Add(container);
    }

    public void AddContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            AddContainer(container);
            
            if (Containers.Count > MaxContainersAmount)
            {
                throw new Exception("Kontenerowiec, nie pomieści więcej kontenerów");
            } 
        }
    }

    public void RemoveContainer(string serialNumber)
    {
        var container = Containers.Find(c => c.SerialNumber == serialNumber);

        if (container == null)
        {
            throw new Exception($"Kontener o numerze seryjnym: {serialNumber} nie jest obecny na kontenerowcu !");
        }
        
        Containers.Remove(container);
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = Containers.Find(c => c.SerialNumber == serialNumber);
        
        if (container == null)
        {
            throw new Exception($"Kontener o numerze seryjnym: {serialNumber} nie jest obecny na kontenerowcu !");
        }
        
        container.UnloadCargo();
    }

    public void ReplaceContainer(string serialNumber, Container newContainer)
    {
        int index = Containers.FindIndex(c => c.SerialNumber == serialNumber);

        if (index == -1)
        {
            throw new Exception($"Kontener o numerze seryjnym: {serialNumber} nie istnieje !");
        }
        
        double currentWeight = CalculateTotalWeight() - (Containers[index].Weight + Containers[index].CargoMass);
        double newWeight = currentWeight + newContainer.Weight + newContainer.CargoMass;
        currentWeight = newWeight;

        if (currentWeight > MaxTotalWeight)
        {
            throw new Exception("Zamiana tych kontenerów spowoduje przekroczenie maksymalnej wagi kontenerów jaką może przewozić kontenerowiec !");
        }
        
        RemoveContainer(serialNumber);
        Containers[index] = newContainer;
    }

    public void TransferContainer(string serialNumber, ContainerShip containerShip)
    {
        var container = Containers.Find(c => c.SerialNumber == serialNumber);

        if (container == null)
        {
            throw new Exception($"Kontener o numerze seryjnym: {serialNumber} nie istnieje !");
        }
        
        RemoveContainer(serialNumber);
        containerShip.AddContainer(container);
    }

    public void ShowContainerDetails(string serialNumber)
    {
        var container = Containers.Find(c => c.SerialNumber == serialNumber);

        if (container == null)
        {
            throw new Exception($"Kontener o numerze seryjnym: {serialNumber} nie istnieje !");
        }

        Console.WriteLine(container.ToString());
    }

    public void ShowShipDetails()
    {
        double totalWeight = CalculateTotalWeight();
        Console.WriteLine($"""
                           Dane kontenerowca: 
                           > maksymalna prędkość: {MaxSpeed} węzłów,
                           > maksymalna ilość kontenerów, jaką może przewieźć: {MaxContainersAmount},
                           > maksymalna waga: {MaxTotalWeight} kg,
                           > aktualna waga obecnych kontenerów na statku: {totalWeight} kg)
                           """);
        Console.WriteLine("Obecne kontenery na kontenerowcu: ");

        foreach (var container in Containers)
        {
            Console.WriteLine(container.ToString());
        }
    }
}