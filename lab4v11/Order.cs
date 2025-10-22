public class Order
{  // Композиція замовлення саме створює кошик
    private Cart cart;        
// Агрегація замовлення знає покупця
    private Customer customer; 

    public Order(Customer customer)
    {
        this.customer = customer;
        // Сама композиція
        this.cart = new Cart(); 
    }

    public void AddProductToOrder(IProduct product)
    {
        cart.AddProduct(product);
    }
// Вивід інформації по замовленню
    public void PrintOrder()
    {
        Console.WriteLine($"Замовлення для: {customer.Name}");
        Console.WriteLine($"Сума: {cart.GetTotalPrice()} грн");
        Console.WriteLine($"Середня ціна: {cart.GetAveragePrice()} грн");
    }
}
