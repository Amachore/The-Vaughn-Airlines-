# The-Vaughn-Airlines-
A simple terminal-based airline simulator. Uses Queues, Dictionary, Lists. First version lacks a search function (as well as other additional functions, but the search function is primary). Modifiable. 

## Application summary

`AirlineBookingSimulator` is a small console app that lets a user create and manage simple flight bookings.

What it does
- Prompts for your name and shows a small menu.
- Lets you create a flight: select a destination, enter departure/return dates and class, then confirm the booking.
- When confirmed, it generates a ticket (sequential number + 6-char code), shows it on-screen, and appends it to `FlightStatuses.txt`.
- While the app runs you can view the log, search for a ticket by number, or cancel an in-memory booking.

How to run
- Requires .NET 9. Open the solution in Visual Studio 2022 or run `dotnet run`.
- Enter your name, choose option `2` to create a flight, follow prompts, confirm with `yes` to save the ticket.

Recent fixes applied
- Log filename corrected to `FlightStatuses.txt`.
- Ticket generation now happens only on confirmation.
- Duplicate confirmation/logging removed.
- Null-safe Console input and more robust log newline handling.


AI Application: Very little (about 3%). I only used Copilot Chat for quickly resolving the errors after they were unfolded when I had opened this project again after nearly a year since I presented it to my professor in my Data Structures and Algorithms class. 
