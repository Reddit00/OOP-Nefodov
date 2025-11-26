using System;
using System.Collections.Generic;
using System.Linq;

//Власний делегат
delegate double Operation(double a, double b); //арифметичний делегат

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }

    public Product(string name, double price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Лабораторна 6: Лямбда та Делегати\n");
        //Використання власного делегата
        Operation add = (a, b) => a + b; // лямбда
        Operation mul = delegate (double x, double y) { return x * y; }; // анонімний метод

        Console.WriteLine("Додавання: " + add(3, 5));
        Console.WriteLine("Множення: " + mul(3, 5));

        //Стандартні делегати

        // Predicate<T> — повертає bool
        Predicate<int> isEven = n => n % 2 == 0;
        // Action<T> — не повертає значення
        Action<string> print = s => Console.WriteLine($"[ВИВІД] {s}");
        // Func<T1, T2, TResult> — повертає TResult
        Func<int, int, int> subtract = (a, b) => a - b;
        print($"4 — парне число? {isEven(4)}");
        print($"Віднімання: {subtract(10, 3)}");

        //LINQ з лямбдами
        List<int> nums = new() { 1, 2, 3, 4, 5, 6 };

        var squared = nums.Select(x => x * x);  // Select із лямбдою
        // Фільтрація парних чисел
        var evens = nums.Where(x => x % 2 == 0);
        print("Квадрати чисел:");
        squared.ToList().ForEach(n => Console.WriteLine(n));

        //Робота з Product
        List<Product> products = new()
        {
            new Product("Філе курки за кг", 88, "М\'ясо"),
            new Product("Сир Гауда за кг", 140, "Сири"),
            new Product("Філе рибиза кг", 110, "М\'ясо"),
            new Product("Сир Чеддер за кг", 170, "Сири"),
            new Product("Яловичина за кг", 70, "М\'ясо"),
            new Product("Моцарела за кг", 90, "Сири")
        };

        //Фільтрація товарів дорожче 100
        var expensive = products.Where(p => p.Price > 100);
        Console.WriteLine("\nТовари дорожче 100:");
        expensive.ToList().ForEach(p =>
        Console.WriteLine($"{p.Name} - {p.Price}"));

        //Найдорожчий товар
        var maxPrice = products.Max(p => p.Price);
        var mostExpensive = products.First(p => p.Price == maxPrice);

        Console.WriteLine($"\nНайдорожчий товар: {mostExpensive.Name} ({mostExpensive.Price})");

        //Середня ціна товарів
        double avg = products.Average(p => p.Price);
        Console.WriteLine($"Середня ціна: {avg:F2}");

        // Сортування за ціною
        var ordered = products.OrderBy(p => p.Price);
        Console.WriteLine("\nСортування за ціною:");
        foreach (var p in ordered)
            Console.WriteLine($"{p.Name}: {p.Price}");
    }
}