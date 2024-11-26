using System;
using System.IO;
using System.Collections.Generic;
using FlightReservationSystemProject;

// ****** Initial Menu and creation/use of submenu objects. *******
class Program
{
 
    
    static void Main(string[] args)
    {
        CustomerMenu customerMenu = new CustomerMenu();
        FlightMenu flightMenu = new FlightMenu();
        BookingMenu bookingMenu = new BookingMenu();
        
        bool RUNNING = true;

        while (RUNNING)
        {
            Console.Clear();
            Console.WriteLine("---- EXtreme Flight Reservation System ----");
            Console.WriteLine("---------------  Main Menu  ---------------");
            Console.WriteLine("\n 1. Customers.");
            Console.WriteLine("\n 2. Flights.");
            Console.WriteLine("\n 3. Bookings. ");
            Console.WriteLine("\n 4. Exit.");
            Console.Write("\n Select an option by entering 1-4: ");
            
            // ? is for the conversion of null literal and/or value into non-nullable type. 
            string userChoice = Console.ReadLine()?.Trim();
            switch (userChoice)
            {
                case "1":
                    customerMenu.DisplayCustomerMenu();
                    break;
                case "2":
                    flightMenu.ShowMenu();
                    break;
                case "3":
                    bookingMenu.ShowMenu();
                    break;
                case "4":
                    if (ConfirmExit())
                    {
                        RUNNING = false;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option please select an option by inputting a number between 1-4.");
                    break;
            }
            
            static bool ConfirmExit()
            {
                Console.Write("Are you sure want to exit (Y/N): ");
                // ? is for the conversion of null literal and/or value into non-nullable type. TODO: Perhaps another way to confirm this? 
                string confirmation = Console.ReadLine()?.Trim().ToUpper() ?? "N";
                return confirmation == "Y";
            }
        }
    }
}




