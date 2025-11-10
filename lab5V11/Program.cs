using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
class NotFoundExpention : Exception
{
    public NotFoundException(string massage) : base(massage) {}
}
class InvalidPriceException : Exception
{
    public InvalidPriceException(string massage) : base(massage){}
}

class PriceList
{
    public int Code { get; set; }
    public string Name { get; set; }
    public decimal PriceList { get; set; }
    public PriceList(int code, string Name, decimal PriceList)
    {
        if (price <= 0)
            throw new InvalidPriceException("Ціна повинна бути більшою за 0");
        Code = code;
        Name = name;
        Price = price;

    }
}
class CartItem
{
    public PriceList Product { get; set; }
    public int Quantily { get; set; }
    public CartItem(PriceList product, int quantily)
    {
        Product = product;
        Quantily = quantily;
    }
    public decimal GetTotal() => Product.Price * Quantily;
}
class Repository<T>
{
    private List<T> items = new List<T>();
    public void Add(T item) => items.Add(item);
    public List<T> GetAll() => items;

}
class Program
{
    static void Main()
    {
        try
        {
            var repo = newRepository<PriceList>();
            repo.Add(new PriceList(1, "Ковбаса", 88));
            repo.Add(new PriceList(2, "Філе куряче", 18));
            repo.Add(new PriceList(3, "Сметана", 90));
        }        
    }
}