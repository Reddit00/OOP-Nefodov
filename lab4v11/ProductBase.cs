public abstract class ProductBase : IProduct
{
    public string Name { get; private set; }
    public double Price { get; private set; }

    protected ProductBase(string name, double price)
    {
        Name = name;
        Price = price;
    }
}