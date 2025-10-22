using System;
class Program
{
    static void Main()
    {
        // Створення покупця (агрегація)
        Customer customer = new Customer("Нефьодов Н.Д.");
        // Створення замовлення (композиція та агрегація)
        Order order = new Order(customer);
        // Декілька продуктів (агрегація)
        order.AddProductToOrder(new Food("Сир", 88));
        order.AddProductToOrder(new Food("Яйця", 14));
        order.AddProductToOrder(new Clothes("Чорна футболка", 177));

        // Вивід результату
        order.PrintOrder();
    }
}