//Корзина товарів
using System.Collections.Generic;
using System.Linq;

public class Cart
{
    private List<IProduct> products = new List<IProduct>();

    public void AddProduct(IProduct item)
    {
        products.Add(item);
    }

//Загальна сума 
    public double GetTotalPrice()
    {
        return products.Sum(p => p.Price);
    }
//Середня ціна
public double GetAveragePrice()
    {
        return products.Count == 0 ? 0 : products.Average(p => p.Price);
    }
}