using System;

namespace AirlineBookingSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine($"Good morning, {name}! I'm VaughnGPT.\n" + "Choose:\n1. Lose weight\n2. Umalis ng Pinas\n3. Bahala na");

            string decision = Console.ReadLine();

            switch (decision)
            {
                case "1":
                case "Lose Weight":
                    Console.WriteLine("\nDiet and exercise lang");
                    break;

                case "2":
                case "Umalis ng Pinas":
                    Console.WriteLine("\nIb'book na kita ng ticket. Pili ka diyan");

                    BookingService service = new BookingService(name);
                    service.CreateFlight();

                    break;

                case "3":
                case "Bahala na":
                    Console.WriteLine("\nEdi wag");
                    break;

                default:
                    Console.WriteLine("\nEdi wag");
                    break;
            }
        }
    }
}