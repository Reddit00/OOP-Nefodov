// using System;
// //BAD LSP
// class Rectangle
// {
//     public virtual int Width { get; set; }
//     public virtual int Height { get; set; }

//     public int GetArea()
//     {
//         return Width * Height;
//     }
// }

// // Похідний клас Square
// // Порушує контракт Rectangle
// // Зміна однієї сторони автоматично змінює іншу
// class Square : Rectangle
// {
//     public override int Width
//     {
//         get => base.Width;
//         set
//         {
//             base.Width = value;
//             base.Height = value;
//         }
//     }
//     public override int Height
//     {
//         get => base.Height;
//         set
//         {
//             base.Width = value;
//             base.Height = value;
//         }
//     }
// }
// // Клієнтський метод, який працює з Rectangle
// // Очікує, що Width і Height незалежні
// static class BadLspClient
// {
//     public static void ResizeRectangle(Rectangle rectangle)
//     {
//         rectangle.Width = 5;
//         rectangle.Height = 10;

//         Console.WriteLine("Expected area: 50");
//         Console.WriteLine($"Actual area: {rectangle.GetArea()}");
//         Console.WriteLine();
//     }
// }
// GOOD LSP
// Спільний інтерфейс для геометричних фігур
interface IShape
{
    int GetArea();
}
// Прямокутник більше не є базовим класом для квадрата
class RectangleLsp : IShape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public int GetArea()
    {
        return Width * Height;
    }
}
// Квадрат має власний контракт
class SquareLsp : IShape
{
    public int Side { get; set; }

    public int GetArea()
    {
        return Side * Side;
    }
}
// Клієнтський метод, який безпечно працює з будь-якою фігурою
static class GoodLspClient
{
    public static void PrintArea(IShape shape)
    {
        Console.WriteLine($"Area: {shape.GetArea()}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== LSP VIOLATION EXAMPLE =====");

        Rectangle rectangle = new Rectangle();
        BadLspClient.ResizeRectangle(rectangle);

        Rectangle square = new Square();
        BadLspClient.ResizeRectangle(square); // ❌ Некоректна поведінка

        Console.WriteLine("===== LSP COMPLIANT SOLUTION =====");

        IShape rectLsp = new RectangleLsp
        {
            Width = 5,
            Height = 10
        };

        IShape squareLsp = new SquareLsp
        {
            Side = 5
        };

        GoodLspClient.PrintArea(rectLsp);
        GoodLspClient.PrintArea(squareLsp);
    }
}
