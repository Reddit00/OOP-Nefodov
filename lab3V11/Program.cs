class Program
{
    static void Main()
    {
        List<Instrument> instruments = new List<Instrument>
        {
           new Piano("Bach", 30),
            new Guitar("Death in June - Runes and Man", 45),
            new Piano("Beethoven - Moonlight Sonata", 40),
            new Guitar("By The Sspirits - Into the Dust", 50)
        };
        int total = 0;

        Console.WriteLine(" Початок концерту\n");
        foreach (var ins in instruments)
        {
            ins.Play();
            total += ins.GetDuration();
        }
        Console.WriteLine($"Загальна тривалість концерту: {total} сек.");
    }
}

