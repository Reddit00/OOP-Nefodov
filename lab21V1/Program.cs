using System;
using System.Linq;
public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Type of tariff (Economy, Standard, Premium, Night): ");
        string? input = Console.ReadLine() ?? "";

        var isValid = Enum.GetValues(typeof(TariffType))
            .Cast<TariffType>()
            .Any(t => t.ToString()
            .Equals(input, StringComparison.OrdinalIgnoreCase));

        if (!isValid)
        {
            Console.WriteLine("Invalid tariff type.");
            return;
        }

        TariffType tariffType =
            (TariffType)Enum.Parse(typeof(TariffType), input, true);

        Console.WriteLine("Distance (km): ");
        string? distanceInput = Console.ReadLine() ?? "0";

        Console.WriteLine("Idle time (minutes): ");
        string? idleInput = Console.ReadLine() ?? "0";

        IShippingStrategy strategy =
            ShippingStrategyFactory.CreateStrategy(tariffType);

        TaxiCalculator calculator = new TaxiCalculator();

        decimal cost = calculator.CalculateCost(
            decimal.Parse(distanceInput),
            decimal.Parse(idleInput),
            strategy);

        Console.WriteLine($"Trip cost: {cost} UAH.");
    }
}
public enum TariffType
{
    Economy,
    Standard,
    Premium,
    Night
}
public interface IShippingStrategy
{
    decimal CalculateCost(decimal distance, decimal idleMinutes);
}
public class EconomyStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal idleMinutes)
    {
        return distance * 5 + idleMinutes * 1;
    }
}
public class StandardStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal idleMinutes)
    {
        return distance * 8 + idleMinutes * 2;
    }
}
public class PremiumStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal idleMinutes)
    {
        return distance * 12 + idleMinutes * 3 + 30;
    }
}

//Демонстрація OCP
public class NightStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal idleMinutes)
    {
        return distance * 8 + idleMinutes * 2 + 40;
    }
}
public static class ShippingStrategyFactory
{
    public static IShippingStrategy CreateStrategy(TariffType type)
    {
        return type switch
        {
            TariffType.Economy  => new EconomyStrategy(),
            TariffType.Standard => new StandardStrategy(),
            TariffType.Premium  => new PremiumStrategy(),
            TariffType.Night    => new NightStrategy(),

            _ => throw new ArgumentException("Invalid tariff type")
        };
    }
}
public class TaxiCalculator
{
    public decimal CalculateCost(
        decimal distance,
        decimal idleMinutes,
        IShippingStrategy strategy)

        => strategy.CalculateCost(distance, idleMinutes);
}
