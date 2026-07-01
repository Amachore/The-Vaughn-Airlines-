using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSimulator
{
    internal class IssueTicket
    {
        private readonly Queue<Ticket> ticketQueue = new Queue<Ticket>();
        private readonly Random rnd = new Random();

        public void PopulateTickets(string passengerName)
        {
            for (int i = 1; i <= 5; i++)
            {
                int ticketNumber = rnd.Next(1000, 9999);
                string flightDetails = $"Flight{i} - CityA to CityB";
                string code = $"CODE{i}";

                Ticket ticket = new Ticket
                {
                    TicketNumber = ticketNumber,
                    PassengerName = passengerName,
                    FlightDetails = flightDetails,
                    Code = code
                };

                ticketQueue.Enqueue(ticket);
            }

            Console.WriteLine("Issued Tickets:");
            while (ticketQueue.Count > 0)
            {
                Console.WriteLine(ticketQueue.Dequeue().FormatTicket());
            }
        }
    }
}

