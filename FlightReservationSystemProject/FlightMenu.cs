using System.Reflection.Metadata;

namespace FlightReservationSystemProject;

public class FlightMenu
{
    private const string CustomersFile = "./customers.txt";
    private const string FlightsFile = "./flights.txt";
    private const string BookingsFile = "./bookings.txt";

    private void AddFlight()
    {
        Console.Clear();
        Console.WriteLine("---- Add Flight ----");

        Console.Write("Enter Flight Number: ");
        if (!int.TryParse(Console.ReadLine(), out int flightNumber))
        {
            Console.WriteLine("Invalid flight number");
            FileAndMenuHelperMethods.Pause();
            return;
        }

        Console.Write("Enter Origin: ");
        string flightOrigin = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(flightOrigin))
        {
            Console.WriteLine("Origin cannot be empty.");
            FileAndMenuHelperMethods.Pause();
            return;
        }
        
        Console.Write("Enter Destination: ");
        string flightDestiantion = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(flightDestiantion))
        {
            Console.WriteLine("Destination cannot be empty.");
            FileAndMenuHelperMethods.Pause();
            return;
        }
        
        Console.Write("Enter Maximum Seats: ");
        if (!int.TryParse(Console.ReadLine(), out int flightMaxSeats) || flightMaxSeats <= 0)
        {
            Console.WriteLine("Invalid number of max seats");
            FileAndMenuHelperMethods.Pause();
            return;
        }
        
        try
        {
            string[] lines = FileAndMenuHelperMethods.ReadFile(FlightsFile);

            // Checking for duplicate flight nums
            if (!ObjectHelperMethods.IsflightNumberUnique(flightNumber, lines))
            {
                Console.WriteLine("Error: Flight num already exists.");
                FileAndMenuHelperMethods.Pause();
                return;
            }

            string newFlightLine = $"{flightNumber}|{flightOrigin}|{flightDestiantion}|{flightMaxSeats}|0";
            FileAndMenuHelperMethods.AppendToFile(FlightsFile, newFlightLine);

            Console.WriteLine($"Flight added successfully! Number: {flightNumber}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }
    
    private void ViewAllFlights()
    {
        Console.Clear();
        Console.WriteLine(" --- View ALL Flights --- ");

        try
        {
            string[] lines = FileAndMenuHelperMethods.ReadFile(FlightsFile);
            if (lines.Length == 0)
            {
                Console.WriteLine("No flights available.");
            }
            else
            {
                foreach (var line in lines)
                {
                    Console.WriteLine(ObjectHelperMethods.ParseFlight(line));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }

    private void ViewSpecificFlight()
    {
        Console.Clear();
        Console.Write("--- View Specific Flight --- ");
        
        Console.Write("Enter valid Flight Number: ");
        if (!int.TryParse(Console.ReadLine(), out int flightNumResult))
        {
            Console.WriteLine("Invalid flight number.");
            FileAndMenuHelperMethods.Pause();
            return;
        }

        try
        {
            string[] lines = FileAndMenuHelperMethods.ReadFile(FlightsFile);
            string flight = ObjectHelperMethods.FindFlightByNumber(flightNumResult, lines);

            if (flight != null)
            {
                Console.WriteLine(ObjectHelperMethods.ParseFlight(flight));
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }

    private void DeleteFight()
    {
        Console.Clear();
        Console.WriteLine("---- Delete Flight ----");
        Console.Write("Enter valid Flight Number to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int flightNum))
        {
            Console.WriteLine("Invalid flight number.");
            FileAndMenuHelperMethods.Pause();
            return;
        }

        try
        {
            string[] lines = FileAndMenuHelperMethods.ReadFile(FlightsFile);
            string[] updatedLines = new string[lines.Length - 1];
            int index = 0;
            bool found = false;

            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                int currentFlightNumber = int.Parse(parts[0]);
                int passangerCount = int.Parse(parts[4]);

                if (currentFlightNumber == flightNum)
                {
                    if (passangerCount > 0)
                    {
                        Console.WriteLine("Cannot delete a flight with booked passangers.");
                        FileAndMenuHelperMethods.Pause();
                        return;
                    }

                    found = true;
                    continue; 
                }
                
                if (index < updatedLines.Length)
                {
                    updatedLines[index++] = line;
                }
            }

            if (!found)
            {
                Console.WriteLine("Flight not found.");
            }
            else
            {
                FileAndMenuHelperMethods.WriteFile(FlightsFile, updatedLines);
                Console.WriteLine("Flight deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting flight: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }
    
    
    bool RUNNING = true;
    public void ShowMenu()
    {
        while (RUNNING)
        {
            Console.Clear();
            Console.WriteLine("---- Flight Menu ----");
            Console.WriteLine("\n Please select a choice from the options below (1-4):");
            Console.WriteLine("\n 1. Add flight");
            Console.WriteLine("\n 2. View Flights");
            Console.WriteLine("\n 3. View Specific Flight");
            Console.WriteLine("\n 4. Delete Flight");
            Console.WriteLine("\n 5. Back to Main Menu");
            Console.Write("\n Select an option:  ");
            
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    AddFlight();
                    break;
                case "2":
                    ViewAllFlights();
                    break;
                case "3":
                    ViewSpecificFlight();
                    break;
                case "4":
                    DeleteFight();
                    break;
                case "5":
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