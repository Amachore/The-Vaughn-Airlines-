using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSimulator
{
    internal class CreatingFlightOptions
    {
        public CreatingFlightOptions() { }
        public string ToLocations()
        {
            List<string> locations = new List<string>
            {
                "Cebu, Philippines",
                "Palawan, Philippines",
                "Seoul, South Korea",
                "Tokyo, Japan",
                "Bangkok, Thailand",
            };
            Console.WriteLine("Available destinations:");
            for (int i = 0; i < locations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {locations[i]}");
            }
            Console.WriteLine("Select a destination by number:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= locations.Count)
            {
                string selected = locations[choice - 1];
                Console.WriteLine($"You selected:{selected}");
                return selected;
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                return ToLocations();
            }
        }
    }
}