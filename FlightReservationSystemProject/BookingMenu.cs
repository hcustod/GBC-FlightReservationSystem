using System.IO.Pipes;
using System.Reflection.Metadata;

namespace FlightReservationSystemProject;

public class BookingMenu
{
    private const string CustomersFile = "./customers.txt";
    private const string FlightsFile = "./flights.txt";
    private const string BookingsFile = "./bookings.txt";
    
    private void AddBooking()
    {
        Console.Clear();
        Console.WriteLine("---- Add Booking ----");
        
        // Load Customers
        string[] customerLines = FileAndMenuHelperMethods.ReadFile(CustomersFile);
        if (customerLines.Length == 0)
        {
            Console.WriteLine("No customer available. Add a customer first.");
            FileAndMenuHelperMethods.Pause();
            return; 
        }
        
        Console.WriteLine("Available Customers:");
        foreach (var line in customerLines)
        {
            Console.WriteLine(ObjectHelperMethods.ParseCustomer(line));
        }
        
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid Customer ID.");
            FileAndMenuHelperMethods.Pause();
            return;
        }
        
        // validate customer
        string customer = ObjectHelperMethods.FindCustomerByID(customerId, customerLines);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            FileAndMenuHelperMethods.Pause();
            return; 
        }
        
        //Load flights
        string[] flightLines = FileAndMenuHelperMethods.ReadFile(FlightsFile);
        if (flightLines.Length == 0)
        {
            Console.WriteLine("No flights available. Add a flight first.");
            FileAndMenuHelperMethods.Pause();
            return;
        }

        Console.WriteLine("\n Available Flights: ");
        foreach (var line in flightLines)
        {
            Console.WriteLine(ObjectHelperMethods.ParseFlight(line));
        }
        
        // Ask for flight number and check if valid. 
        Console.WriteLine("Enter Flight Number: ");
        if (!int.TryParse(Console.ReadLine(), out int flightNum))
        {
            Console.WriteLine("Invalid Flight Number.");
            FileAndMenuHelperMethods.Pause();
            return;
        }
        
        // Validate Flight
        string flight = ObjectHelperMethods.FindFlightByNumber(flightNum, flightLines);
        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            FileAndMenuHelperMethods.Pause();
            return;
        }

        string[] flightParts = flight.Split('|');
        int maxSeats = int.Parse(flightParts[3]);
        int currentPassengers = int.Parse(flightParts[4]);
        if (currentPassengers >= maxSeats)
        {
            Console.WriteLine("Flight is fully booked.");
            FileAndMenuHelperMethods.Pause();
            return;
        }
        
        // Create booking
        try
        {
            string[] bookingLines = FileAndMenuHelperMethods.ReadFile(BookingsFile);
            int bookingID = bookingLines.Length + 1;

            string newBookingLine = $"{bookingID}|{DateTime.Now}|{customerId}|{flightNum}";
            FileAndMenuHelperMethods.AppendToFile(BookingsFile, newBookingLine);
            
            // Update Flight passenger count TODO: rename updatedFlights vars
            string[] updatedFlights = new string[flightLines.Length];
            for (int i = 0; i < flightLines.Length; i++)
            {
                if (flightLines[i].StartsWith(flightNum.ToString()))
                {
                    string updatedFlight =
                        $"{flightParts[0]}|{flightParts[1]}|{flightParts[2]}|{flightParts[3]}|{currentPassengers + 1}";
                    updatedFlights[i] = updatedFlight;
                }
                else
                {
                    updatedFlights[i] = flightLines[i];
                }
            }
            FileAndMenuHelperMethods.WriteFile(FlightsFile, updatedFlights);
            Console.WriteLine($"Booking created successfuly! Booking ID: {bookingID}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        FileAndMenuHelperMethods.Pause();
    }

    private void ViewAllBookings()
    {
        Console.Clear();
        Console.WriteLine("---- View All Bookings ----");

        try
        {
            string[] lines = FileAndMenuHelperMethods.ReadFile(BookingsFile);
            if (lines.Length == 0)
            {
                Console.WriteLine("No bookings available.");
            }
            else
            {
                foreach (var line in lines)
                {
                    string[] parts = line.Split('|');
                    Console.WriteLine(
                        $"Booking ID: {parts[0]}, Date: {parts[1]}, Customer ID: {parts[2]}, Flight Number: {parts[3]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading bookings: {ex.Message}");
        }

        FileAndMenuHelperMethods.Pause();
    }
    
    
    private bool RUNNING = true;
    public void ShowMenu()
    {
        while (RUNNING)
        {
            Console.Clear();
            Console.WriteLine("---- Bookings Menu ----");
            Console.WriteLine("\n Please select a choice from the options below (1-4):");
            Console.WriteLine("\n 1. Add Booking.");
            Console.WriteLine("\n 2. View All Bookings.");
            Console.WriteLine("\n 3. Back to Main Menu. ");
            Console.Write("Select an option: ");
            
            string userChoice = Console.ReadLine()?.Trim();
            switch (userChoice)
            {
                case "1":
                    AddBooking();
                    break;
                case "2":
                    ViewAllBookings();
                    break;
                case "3":
                    if (FileAndMenuHelperMethods.ConfirmReturnToMainMenu())
                    {
                        RUNNING = false; 
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option please select an option by inputting a number between 1-4.");
                    FileAndMenuHelperMethods.Pause();
                    break;
            }
        }
    }
}