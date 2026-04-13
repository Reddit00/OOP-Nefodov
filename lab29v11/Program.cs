using System.Diagnostics;
namespace lab29v11;

class Program
{
    static async Task Main(string[] args)
    {
        string bigFile = "visits.csv";
        string filteredFile = "high_activity_visits.csv";
        int rowCount = 1_000_000; // 1 мільйон рядків

        // 1. Генерація файлу
        Console.WriteLine("Генерація великого файлу...");
        await GenerateBigFileAsync(bigFile, rowCount);

        // 2. Порівняння продуктивності
        Console.WriteLine("\nПорівняння продуктивності читання файлу");
        
        // Синхронне читання
        var sw = Stopwatch.StartNew();
        int syncCount = ReadFileSync(bigFile);
        sw.Stop();
        Console.WriteLine($"Синхронне читання: {sw.ElapsedMilliseconds} ms | Рядків: {syncCount}");

        // Асинхронне читання
        sw.Restart();
        int asyncCount = await ReadFileAsync(bigFile);
        sw.Stop();
        Console.WriteLine($"Асинхронне читання: {sw.ElapsedMilliseconds} ms | Рядків: {asyncCount}");

        // 3. Фільтрація та запис
        Console.WriteLine("\nФільтрація візитів тривалістю > 100 хв та запис у файл...");
        await FilterAndSaveAsync(bigFile, filteredFile, 100);

        Console.WriteLine("Готово!");
    }

    // МЕТОД 1: Генератор великого файлу 
    static async Task GenerateBigFileAsync(string filename, int count)
    {
        using StreamWriter writer = new StreamWriter(filename);
        await writer.WriteLineAsync("Id,UserId,DurationMinutes,Timestamp");
        Random rnd = new Random();

        for (int i = 1; i <= count; i++)
        {
            await writer.WriteLineAsync($"{i},User{rnd.Next(1, 1000)},{rnd.Next(1, 200)},{DateTime.Now.AddMinutes(-i)}");
        }
    }

    // МЕТОД 2: Асинхронне потокове читання 
    static async Task<int> ReadFileAsync(string filename)
    {
        int count = 0;
        using StreamReader reader = new StreamReader(filename);
        
        await reader.ReadLineAsync();

        while (await reader.ReadLineAsync() != null)
        {
            count++;
        }
        return count;
    }
    static int ReadFileSync(string filename)
    {
        int count = 0;
        using StreamReader reader = new StreamReader(filename);
        reader.ReadLine();
        while (reader.ReadLine() != null)
        {
            count++;
        }
        return count;
    }

    static async Task FilterAndSaveAsync(string input, string output, int minDuration)
    {
        using StreamReader reader = new StreamReader(input);
        using StreamWriter writer = new StreamWriter(output);

        string? header = await reader.ReadLineAsync();
        if (header != null) await writer.WriteLineAsync(header);

        while (await reader.ReadLineAsync() is string line)
        {
            var parts = line.Split(',');
            if (parts.Length >= 3 && int.TryParse(parts[2], out int duration))
            {
                if (duration > minDuration)
                {
                    await writer.WriteLineAsync(line);
                }
            }
        }
    }
}