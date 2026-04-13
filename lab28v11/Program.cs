using lab28v11;
var repo = new CarRepository();

repo.Add(new Car 
{ 
    Id = 1, 
    Model = "Model S", 
    Year = 2023, 
    Brand = new Manufacturer { Name = "Tesla", Country = "USA" },
    CarEngine = new Engine { Volume = 0.0, FuelType = "Electric" }
});

repo.Add(new Car 
{ 
    Id = 2, 
    Model = "M5", 
    Year = 2022, 
    Brand = new Manufacturer { Name = "BMW", Country = "Germany" },
    CarEngine = new Engine { Volume = 4.4, FuelType = "Petrol" }
});

string fileName = "cars_data.json";

await repo.SaveToFileAsync(fileName);

Console.WriteLine("\nОчищення поточних даних та завантаження з файлу");
var freshRepo = new CarRepository();
await freshRepo.LoadFromFileAsync(fileName);

var allCars = freshRepo.GetAll();
foreach (var car in allCars)
{
    Console.WriteLine($"Авто: {car.Brand?.Name} {car.Model} | Рік: {car.Year} | Двигун: {car.CarEngine?.Volume}L ({car.CarEngine?.FuelType})");
}