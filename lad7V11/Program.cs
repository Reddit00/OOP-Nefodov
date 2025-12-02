#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
public class FileProcessor
{
    private int callCount = 0;
    //Метод може викидати FileNotFoundException та IOException
    public List<string> LoadProductNames(string path)
    {
        callCount++;
        Console.WriteLine("[FileProcessor] Спроба читання файлу #" + callCount);

        //Перші 2 рази імітуємо FileNotFoundException
        if (callCount <= 2)
        {
            throw new FileNotFoundException("Файл " + path + " тимчасово недоступний.");
        }

        //Третій раз імітуємо IOException
        if (path.Contains("ioerror"))
        {
            throw new IOException("Загальна помилка вводу та виводу при доступі до файлу.");
        }

        //Якщо помилок немає – повертає список продуктів
        return new List<string>
        {
            "Яблуко",
            "Банан",
            "Апельсин"
        };
    }
}
//Клас, що імітує мережеві запити
public class NetworkClient
{
    private int requestCount = 0;

    //Метод може викидати HttpRequestException
    public List<string> GetProductsFromApi(string url)
    {
        requestCount++;
        Console.WriteLine("[NetworkClient] Спроба запиту #" + requestCount);

        //Перші 3 рази імітуємо HttpRequestException
        if (requestCount <= 3)
        {
            throw new HttpRequestException("Тимчасова помилка доступу до " + url);
        }

        //Якщо помилок немає – повертає список продуктів
        return new List<string>
        {
            "Молоко",
            "Хліб",
            "Сир"
        };
    }
}

//Узагальнений допоміжний клас для реалізації патерну Retry
public static class RetryHelper
{
    //Метод виконує операцію з повторними спробами
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool> shouldRetry = null)
    {
        //Якщо затримку не передали – 500 мс за замовчуванням
        if (initialDelay == default)
        {
            initialDelay = TimeSpan.FromMilliseconds(500);
        }
        //Початкова затримка
        TimeSpan delay = initialDelay;

        for (int attempt = 1; attempt <= retryCount; attempt++)
        {
            try
            {
                Console.WriteLine("[Retry] Спроба №" + attempt);
                T result = operation();
                Console.WriteLine("[Retry] Операція успішна на спробі №" + attempt);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Retry] Помилка: " + ex.GetType().Name + " - " + ex.Message);

                bool canRetry;

                //Якщо shouldRetry не вказаний – повторення для всіх винятків
                if (shouldRetry == null)
                {
                    canRetry = true;
                }
                else
                {
                    //Використання делегату для визначення, чи можна повторювати
                    canRetry = shouldRetry(ex);
                }

                //Якщо досягли ліміту спроб або не можна повторювати – викидаємо виняток вище
                if (attempt == retryCount || !canRetry)
                {
                    Console.WriteLine("[Retry] Більше не повторюємо. Передаємо виняток вище.");
                    throw;
                }
                //Логування інформації про затримку
                Console.WriteLine("[Retry] Чекаємо " + delay.TotalMilliseconds + " мс перед наступною спробою.\n");
                // Затримка між спробами (експоненційна)
                Thread.Sleep(delay);
                delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * 2);
            }
            finally
            {
                // Просто показуємо, що finally викликається завжди
                 Console.WriteLine("[Retry] finally відпрацював.");
            }
        }
        // Сюди не маємо потрапити, але компілятор вимагає повернення значення
        throw new InvalidOperationException("Операція не була виконана навіть після повторних спроб.");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        //Зламана логіка для демонстрації Retry
        FileProcessor fileProcessor = new FileProcessor();
        NetworkClient networkClient = new NetworkClient();

        // Делегат, що каже, для яких винятків ми хочемо робити retry
        Func<Exception, bool> retryForFileAndHttp = ex =>
            ex is FileNotFoundException || ex is HttpRequestException;
        //Сценарій з файлом
        Console.WriteLine("Сценарій: Отримання списку продуктів з файлу\n");
        try
        {
            List<string> fileProducts = RetryHelper.ExecuteWithRetry(
                () => fileProcessor.LoadProductNames("products.txt"),
                retryCount: 5,
                initialDelay: TimeSpan.FromMilliseconds(500),
                shouldRetry: retryForFileAndHttp);

            Console.WriteLine("\nРезультат читання файлу:");
            foreach (string product in fileProducts)
            {
                Console.WriteLine(" - " + product);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nКінцева помилка при роботі з файлом: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("[Main] finally для файлу відпрацював.\n");
        }
        //Cценарій з мережевим запитом
        Console.WriteLine("Сценарій: Отримання списку продуктів з API\n");

        try
        {
            List<string> apiProducts = RetryHelper.ExecuteWithRetry(
                () => networkClient.GetProductsFromApi("https://api.example.com/products"),
                retryCount: 5,
                initialDelay: TimeSpan.FromMilliseconds(500),
                shouldRetry: retryForFileAndHttp);
            //Виводимо отримані продукти
            Console.WriteLine("\nРезультат запиту до API:");
            foreach (string product in apiProducts)
            {
                Console.WriteLine(" - " + product);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nКінцева помилка при запиті до API: " + ex.Message);
        }
        finally  
        {
            Console.WriteLine("[Main] finally для мережевого запиту відпрацював.");
        }
        //Затримка перед завершенням
        Console.WriteLine("\nНатисніть щось для завершення");
        Console.ReadKey();
    }
}