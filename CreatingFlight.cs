using AirlineBookingSimulator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
internal class BookingService
{
    private const string LogFilename = "FlightStatuses.txt";
    private int nextTicketNumber = 1;
    private readonly Random rnd = new Random();
    private readonly string passengerName;
    private readonly Dictionary<int, Ticket> confirmedTickets = new Dictionary<int, Ticket>();

    public BookingService(string name)
    {
        passengerName = name;
    }

    public void CreateFlight()
    {
        Console.WriteLine("Enter the following details:\nFrom:");
        string from = Console.ReadLine() ?? string.Empty;
        CreatingFlightOptions flightOptions = new CreatingFlightOptions();
        string to = flightOptions.ToLocations();

        Console.WriteLine("Date of Departure (MM/DD/YYYY):");
        string depart = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Date of Return (MM/DD/YYYY):");
        string ret = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Class (Economy/Business/First):");
        string flightClass = Console.ReadLine() ?? string.Empty;

        string flightDetails = "From: " + from + " | To: " + to + " | Departure: " + depart + " | Return: " + ret + " | Class: " + flightClass;
        Console.WriteLine("\n\t\t\tBooking Summary:");
        Console.WriteLine(flightDetails);

        Console.WriteLine("\nMay I book your flight? (yes/no)");
        string confirmation = Console.ReadLine();
        if (confirmation != null) confirmation = confirmation.Trim().ToLower();

        if (confirmation == "yes")
        {
            Ticket ticket = GenerateTicket(flightDetails);
            confirmedTickets.Add(ticket.TicketNumber, ticket);
            LogWrite(ticket.FormatTicket());
            Console.WriteLine("\nYour booking has been confirmed. Here is your ticket:");
            Console.WriteLine(ticket.FormatTicket());
        }
        else
        {
            Console.WriteLine("Booking cancelled.");
        }

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nWhat to do next, " + passengerName);
            Console.WriteLine("1. View Current Bookings");
            Console.WriteLine("2. Cancel a Booking");
            Console.WriteLine("3. Search by flight ticket number");
            Console.WriteLine("4. Exit");

            string option = Console.ReadLine();

            if (option == "1")
            {
                ReadLog();
            }
            else if (option == "2")
            {
                Console.WriteLine("Enter ticket number to cancel:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number) && confirmedTickets.ContainsKey(number))
                {
                    confirmedTickets.Remove(number);
                    Console.WriteLine("Booking cancelled.");
                }
                else
                {
                    Console.WriteLine("Ticket not found.");
                }
            }
            else if (option == "3")
            {
                Console.WriteLine("Enter ticket number to search:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number) && confirmedTickets.ContainsKey(number))
                {
                    Console.WriteLine("Flight details:");
                    Console.WriteLine(confirmedTickets[number].FormatTicket());
                }
                else
                {
                    Console.WriteLine("Ticket not found.");
                }
            }
            else if (option == "4")
            {
                running = false;
            }
        }
    }

    private Ticket GenerateTicket(string flightDetails)
    {
        string code = GenerateCode();
        return new Ticket
        {
            TicketNumber = nextTicketNumber++,
            PassengerName = passengerName,
            FlightDetails = flightDetails,
            Code = code
        };
    }

    private string GenerateCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] buffer = new char[6];
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = chars[rnd.Next(chars.Length)];
        }
        return new string(buffer);
    }

    private void LogWrite(string entry)
    {
        try
        {
            File.AppendAllText(LogFilename, entry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to write log: " + ex.Message);
        }
    }

    public void ReadLog()
    {
        if (!File.Exists(LogFilename))
        {
            Console.WriteLine("\tNo log file found.");
            return;
        }

        foreach (string line in File.ReadAllLines(LogFilename))
        {
            Console.WriteLine("\t" + line);
        }
    }
}
