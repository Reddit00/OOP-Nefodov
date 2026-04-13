namespace lab28v11;
public class Manufacturer
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class Engine
{
    public double Volume { get; set; }
    public string FuelType { get; set; } = "Petrol";
}

public class Car
{
    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public Manufacturer? Brand { get; set; }
    public Engine? CarEngine { get; set; }
}