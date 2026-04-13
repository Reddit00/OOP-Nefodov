using System.Text.Json;
namespace lab28v11;

public class CarRepository
{
    private List<Car> _cars = new();
    
    private readonly JsonSerializerOptions _options = new() 
    { 
        WriteIndented = true,
        PropertyNameCaseInsensitive = true 
    };

    public void Add(Car car) => _cars.Add(car);

    public List<Car> GetAll() => _cars;

    public Car? GetById(int id) => _cars.FirstOrDefault(c => c.Id == id);

    public async Task SaveToFileAsync(string filename)
    {
        using FileStream fs = File.Create(filename);
        await JsonSerializer.SerializeAsync(fs, _cars, _options);
        Console.WriteLine($"[System] Дані серіалізовано у файл: {filename}");
    }
    public async Task LoadFromFileAsync(string filename)
    {
        if (!File.Exists(filename)) return;

        using FileStream fs = File.OpenRead(filename);
        _cars = await JsonSerializer.DeserializeAsync<List<Car>>(fs, _options) ?? new();
        Console.WriteLine($"[System] Дані десеріалізовано з файлу: {filename}");
    }
}