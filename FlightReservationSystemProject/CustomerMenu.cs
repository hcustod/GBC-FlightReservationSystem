namespace FlightReservationSystemProject;

public class CustomerMenu
{
    private const string CustomerFile = "./customers.txt";
    
    bool RUNNING = true;
    public void DisplayCustomerMenu()
    {
        while (RUNNING)
        {
            Console.Clear();
            Console.WriteLine("---- EXtreme Flight Reservation System ----");
            Console.WriteLine("---------------  Customer Menu  ---------------");
            Console.WriteLine("\n Please select a choice from the options below (1-4):");
            Console.WriteLine("\n 1. Add Customer.");
            Console.WriteLine("\n 2. View all Customers.");
            Console.WriteLine("\n 3. Delete Customer. ");
            Console.WriteLine("\n 4. Back to Main Menu.");
            
            string userChoice = Console.ReadLine()?.Trim();
            switch (userChoice)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    ViewCustomers();
                    break;
                case "3":
                    DeleteCustomer();
                    break;
                case "4":
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
    
    // TODO: Add customer should first call to check for duplicate customerID. 
    // If customer does not exist on file it should then create a customer object with user input.
    // Customer object to string.
    // string to array. 
    // array to saving in customers.txt. 
    private void AddCustomer()
    {
        Console.Clear();
        Console.WriteLine(" ---- Add Customer ----");
        Console.Write("Enter First Name: ");
        string firstNameGiven = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string lastNameGiven = Console.ReadLine();
        Console.Write("Enter Phone Number: ");
        string phoneNumberGiven = Console.ReadLine();

        try
        {
            Console.Clear();
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine()?.Trim();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine()?.Trim();
            Console.WriteLine("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine()?.Trim();

            CustomerAcc newCustomer = new CustomerAcc(firstName, lastName, phoneNumber);
            FileAndMenuHelperMethods.AddCustomer(CustomerFile, newCustomer);
            
            Console.WriteLine("Customer added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }

    // View all customers in the customers.txt file. 
    private void ViewCustomers()
    {
        Console.Clear();
        Console.WriteLine("--- View Customers ---");

        try
        {
            string[] customers = FileAndMenuHelperMethods.ReadFile(CustomerFile);
            if (customers.Length == 0)
            {
                Console.WriteLine("No customers found.");
            }
            else
            {
                foreach (string line in customers)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length >= 5)
                    {
                        Console.WriteLine($"ID: {parts[0]}, Name: {parts[1]} {parts[2]}, Phone: {parts[3]}, Bookings: {parts[4]}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }

    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("---- Delete Customer ----");
        Console.WriteLine("Enter a valid Customer ID to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            FileAndMenuHelperMethods.Pause();
            return;
        }

        try
        {
            string[] lines = FileAndMenuHelperMethods.ReadFile(CustomerFile);
            string[] updatedLines = new string[lines.Length];
            int index = 0;
            bool found = false;

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (int.TryParse(parts[0], out int customerID) && customerID == id)
                {
                    if (int.Parse(parts[4]) > 0)
                    {
                        Console.WriteLine("Cannot delete customer with existing bookings.");
                        FileAndMenuHelperMethods.Pause();
                        return;
                    }

                    found = true; 
                }
                else
                {
                    updatedLines[index++] = line; 
                }
            }

            if (!found)
            {
                Console.WriteLine("Customer not found");
            }
            else
            {
                string[] finalLines = new string[index];
                Array.Copy(updatedLines, finalLines, index);
                FileAndMenuHelperMethods.WriteFile(CustomerFile, finalLines);
                Console.WriteLine("Customer deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        FileAndMenuHelperMethods.Pause();
    }
}
