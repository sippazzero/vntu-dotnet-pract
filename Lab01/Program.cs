using System;
using System.Globalization;

namespace ConverterApp
{
    internal class Program
    {
       
        private const double KmPerMile = 1.609344;      
        private const double KgPerPound = 0.45359237;   

        private const double AbsoluteZeroCelsius = -273.15;
        private const double AbsoluteZeroFahrenheit = -459.67;

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool running = true;

            while (running)
            {
                ShowMenu();
                int choice = ReadMenuChoice();

                switch (choice)
                {
                    case 1:
                        running = false;
                        Console.WriteLine("До побачення!");
                        break;

                    case 2:
                        ConvertKmToMiles();
                        break;

                    case 3:
                        ConvertMilesToKm();
                        break;

                    case 4:
                        ConvertKgToPounds();
                        break;

                    case 5:
                        ConvertPoundsToKg();
                        break;

                    case 6:
                        ConvertCelsiusToFahrenheit();
                        break;

                    case 7:
                        ConvertFahrenheitToCelsius();
                        break;

                    default:
                        Console.WriteLine("Невірний пункт меню. Спробуйте ще раз.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("Натисніть Enter, щоб продовжити...");
                    Console.ReadLine();
                    Console.WriteLine();
                }
            }
        }

      

        private static void ShowMenu()
        {
            Console.WriteLine("========== Конвертер величин ==========");
            Console.WriteLine("1. Вихід");
            Console.WriteLine("2. км -> милі");
            Console.WriteLine("3. милі -> км");
            Console.WriteLine("4. кг -> фунти");
            Console.WriteLine("5. фунти -> кг");
            Console.WriteLine("6. °C -> °F");
            Console.WriteLine("7. °F -> °C");
            Console.WriteLine("========================================");
            Console.Write("Оберіть пункт меню: ");
        }

        private static int ReadMenuChoice()
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }

            return -1; 
        }

       

        private static double ReadDoubleValue(string prompt)
        {
            double value;
            bool isValid;

            do
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                isValid = double.TryParse(
                    input,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out value);

                if (!isValid)
                {
                    Console.WriteLine("Помилка: введіть коректне число (наприклад, 12.5).");
                }
            }
            while (!isValid);

            return value;
        }
        private static double KmToMiles(double km) => km / KmPerMile;

        private static double MilesToKm(double miles) => miles * KmPerMile;

        private static double KgToPounds(double kg) => kg / KgPerPound;

        private static double PoundsToKg(double pounds) => pounds * KgPerPound;

        private static double CelsiusToFahrenheit(double celsius) => celsius * 9.0 / 5.0 + 32.0;

        private static double FahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32.0) * 5.0 / 9.0;

        // ---------- Обробники пунктів меню ----------

        private static void ConvertKmToMiles()
        {
            double km = ReadDoubleValue("Введіть відстань у кілометрах: ");

            if (!IsNonNegative(km, "Відстань"))
            {
                return;
            }

            double miles = KmToMiles(km);
            Console.WriteLine($"{km:F2} км = {miles:F2} милі");
        }

        private static void ConvertMilesToKm()
        {
            double miles = ReadDoubleValue("Введіть відстань у милях: ");

            if (!IsNonNegative(miles, "Відстань"))
            {
                return;
            }

            double km = MilesToKm(miles);
            Console.WriteLine($"{miles:F2} милі = {km:F2} км");
        }

        private static void ConvertKgToPounds()
        {
            double kg = ReadDoubleValue("Введіть масу в кілограмах: ");

            if (!IsNonNegative(kg, "Маса"))
            {
                return;
            }

            double pounds = KgToPounds(kg);
            Console.WriteLine($"{kg:F2} кг = {pounds:F2} фунти");
        }

        private static void ConvertPoundsToKg()
        {
            double pounds = ReadDoubleValue("Введіть масу у фунтах: ");

            if (!IsNonNegative(pounds, "Маса"))
            {
                return;
            }

            double kg = PoundsToKg(pounds);
            Console.WriteLine($"{pounds:F2} фунти = {kg:F2} кг");
        }

        private static void ConvertCelsiusToFahrenheit()
        {
            double celsius = ReadDoubleValue("Введіть температуру у °C: ");

            if (celsius < AbsoluteZeroCelsius)
            {
                Console.WriteLine(
                    $"Попередження: температура не може бути нижчою за абсолютний нуль ({AbsoluteZeroCelsius} °C). Конвертацію скасовано.");
                return;
            }

            double fahrenheit = CelsiusToFahrenheit(celsius);
            Console.WriteLine($"{celsius:F2} °C = {fahrenheit:F2} °F");
        }

        private static void ConvertFahrenheitToCelsius()
        {
            double fahrenheit = ReadDoubleValue("Введіть температуру у °F: ");

            if (fahrenheit < AbsoluteZeroFahrenheit)
            {
                Console.WriteLine(
                    $"Попередження: температура не може бути нижчою за абсолютний нуль ({AbsoluteZeroFahrenheit} °F). Конвертацію скасовано.");
                return;
            }

            double celsius = FahrenheitToCelsius(fahrenheit);
            Console.WriteLine($"{fahrenheit:F2} °F = {celsius:F2} °C");
        }

        // ---------- Валідація граничних випадків ----------

        /// <summary>
        /// Перевіряє, що значення невід'ємне (для відстані та маси).
        /// Якщо значення від'ємне - виводить попередження і повертає false.
        /// </summary>
        private static bool IsNonNegative(double value, string quantityName)
        {
            if (value < 0)
            {
                Console.WriteLine($"Попередження: {quantityName} не може бути від'ємною. Конвертацію скасовано.");
                return false;
            }

            return true;
        }
    }
}
