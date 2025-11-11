using System;
using System.Collections.Generic;
using System.Linq;

//Мої винятки 
public class InvalidItemException : Exception
{
    public InvalidItemException(string message) : base(message) { }
}
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
//Інтерфейс
public interface IRepository<T>
{
    void Add(T item);
    bool Remove(Predicate<T> match);
    IEnumerable<T> Where(Func<T, bool> predicate);
    T? FirstOrDefault(Func<T, bool> predicate);
    IReadOnlyList<T> All();
}
//Репозиторій
public class Repository<T> : IRepository<T>
{
    private readonly List<T> _data = new();
    public void Add(T item) => _data.Add(item);
    public bool Remove(Predicate<T> match) => _data.RemoveAll(match) > 0;
    public IEnumerable<T> Where(Func<T, bool> predicate) => _data.Where(predicate);
    public T? FirstOrDefault(Func<T, bool> predicate) => _data.FirstOrDefault(predicate);
    public IReadOnlyList<T> All() => _data;
}
//Сутність 1
public class PriceList
{
    public int Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public PriceList(int code, string name, decimal price)
    {
        if (price <= 0)
            throw new InvalidItemException($"Некоректна ціна для товару: {name}");
        Code = code;
        Name = name;
        Price = price;
    }
}
//Сутність 2
public class CartItem
{
    public PriceList Product { get; set; } // агрегація
    public int Quantity { get; set; }
    public CartItem(PriceList product, int quantity)
    {
        if (quantity <= 0)
            throw new InvalidItemException("Кількість повинна бути більшою за 0");

        Product = product;
        Quantity = quantity;
    }
    public decimal GetTotal() => Product.Price * Quantity;
}
class Program
{
    static void Main()
    {
        try
        {   //Прайс лист
            IRepository<PriceList> repo = new Repository<PriceList>();
            repo.Add(new PriceList(1, "Ковбаса", 88));
            repo.Add(new PriceList(2, "Філе куряче", 140));
            repo.Add(new PriceList(3, "Сметана", 90));

            Console.WriteLine("Прайс-лист");
            foreach (var p in repo.All())
                Console.WriteLine($"{p.Code}. {p.Name} - {p.Price} грн");

            //Кошик
            var cart = new List<CartItem>
            {
                new CartItem(repo.FirstOrDefault(p => p.Code == 1)!, 2),
                new CartItem(repo.FirstOrDefault(p => p.Code == 2)!, 1)
            };

            // обчислення
            decimal sum = cart.Sum(i => i.GetTotal());
            decimal discount = sum * 0.07m;
            decimal total = sum - discount;
            decimal avgPrice = cart.Average(i => i.Product.Price);

            Console.WriteLine("\nКошик");
            foreach (var item in cart)
                Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {item.GetTotal()} грн");

            Console.WriteLine($"\nСума: {sum} грн");
            Console.WriteLine($"Знижка 7%: -{discount:F2} грн");
            Console.WriteLine($"До сплати: {total:F2} грн");
            Console.WriteLine($"Середня ціна: {avgPrice:F2} грн");
            repo.Add(new PriceList(4, "Пиво", -100)); //Викликає виняток
        }
        //Обробка винятків
        catch (InvalidItemException ex)
        {
            Console.WriteLine($"\nПомилка: {ex.Message}");
        }
        catch (NotFoundException ex)
        {
            Console.WriteLine($"\nПомилка пошуку: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nІнша помилка: {ex.Message}");
        }
    }
}