using AirlineBookingSimulator;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
internal class BookingService
{
    private const string LogFilename = "FlightStatusess.txt";
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
        string from = Console.ReadLine();
        CreatingFlightOptions flightOptions = new CreatingFlightOptions();
        string to = flightOptions.ToLocations();

        Console.WriteLine("Date of Departure (MM/DD/YYYY):");
        string depart = Console.ReadLine();
        Console.WriteLine("Date of Return (MM/DD/YYYY):");                                   
        string ret = Console.ReadLine();
        Console.WriteLine("Class (Economy/Business/First):");
        string flightClass = Console.ReadLine();

        string flightDetails = $"From: {from} | To: {to} | Departure: {depart} | Return: {ret} | Class: {flightClass}";
        Console.WriteLine("\n\t\t\tBooking Summary:");
        Console.WriteLine(flightDetails);

        Console.WriteLine("\nMay I book your flight? (yes/no)");
        string confirmation = Console.ReadLine()?.Trim().ToLower();
        Ticket ticket = GenerateTicket(flightDetails); 

        if (confirmation == "yes")
        {
            confirmedTickets.Add(ticket.TicketNumber, ticket);
            LogWrite(ticket.FormatTicket());
            Console.WriteLine("\nYour booking has been confirmed. Here is your ticket:");
            Console.WriteLine(ticket.FormatTicket());
        }
        else
        {
            Console.WriteLine("Booking cancelled.");
        }

        Console.WriteLine("\nWhat to do next, " + passengerName);
        Console.WriteLine("1. View Current Bookings");
        Console.WriteLine("2. Cancel a Booking");
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
            int number;
            if (int.TryParse(input, out number) && confirmedTickets.ContainsKey(number))
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
            AddAnotherBooking();
        }
    }
    public void AddAnotherBooking()
    {
        CreateFlight();
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
            File.AppendAllText(LogFilename, entry + "\n");
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
