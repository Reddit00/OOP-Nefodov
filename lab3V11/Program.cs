class Program
{
    static void Main()
    {
        List<Instrument> instruments = new List<Instrument>
        {
            new Guitar("Fender"),
            new Piano("Yamaha"),
            new Guitar("Gibson"),
            new Piano("Casio")
        };
        int total = 0;
        foreach (var ins in instruments)
        {
            ins.Play();
            int time = ins.GetTotalTime();
            Console.WriteLine($"Час: {time} хв\n");
            total += time;
        }
        Console.WriteLine($"Загальна тривалість концерту: {total} хв");
    }
}
