class Program
{
    static void Main()
    {
        Temperature t1 = new Temperature { Celsius = 20 };
        Temperature t2 = new Temperature { Celsius = -5 };
        Console.WriteLine($"t1 у Фаренгейтах: {t1.Fahrenheit}");
        Console.WriteLine($"t2 у Фаренгейтах (через індексатор): {t2[1]}");
        if (t1 > t2) Console.WriteLine("t1 тепліший за t2");
        if (t1 == t2) Console.WriteLine("t1 дорівнює t2");
    }
}
