using System;

namespace AirlineBookingSimulator
{
    internal class Ticket
    {
        public int TicketNumber { get; set; }
        public string PassengerName { get; set; }
        public string FlightDetails { get; set; }
        public string Code { get; set; }

        public string FormatTicket()
        {
            return $"{TicketNumber:D4}-{Code} | {PassengerName} | {FlightDetails}";
        }
    }
}