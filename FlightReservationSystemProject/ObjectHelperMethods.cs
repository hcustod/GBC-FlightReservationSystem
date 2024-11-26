namespace FlightReservationSystemProject;

using System;
using System.Collections.Generic;

public class ObjectHelperMethods
{
    // Find customer by ID
    public static string FindCustomerByID(int id, string[] lines)
    {
        foreach (var line in lines)
        {
            string[] parts = line.Split('|');
            if (int.Parse(parts[0]) == id)
            {
                return line;
            }
        }
        
        return null;
    }

    public static bool IsCustomerIDUnique(int id, string[] lines)
    {
        return FindCustomerByID(id, lines) == null; 
    }

    // Parses customer record from txt file into string
    public static string ParseCustomer(string line_p)
    {
        string[] parts = line_p.Split('|');
        return $"ID: {parts[0]}, Name: {parts[1]} {parts[2]}, Phone: {parts[3]}, Bookings: {parts[4]}";
    }

    public static string FindFlightByNumber(int flightNumber_p, string[] lines)
    {
        foreach (var line in lines)
        {
            string[] parts = line.Split('|');
            if (int.Parse(parts[0]) == flightNumber_p)
            {
                return line; 
            }
        }

        return null; 
    }

    public static bool IsflightNumberUnique(int flightNum_p, string[] lines)
    {
        return FindFlightByNumber(flightNum_p, lines) == null;
    }

    public static string ParseFlight(string line)
    {
        string[] parts = line.Split('|');
        return $"Number: {parts[0]}, Origin: {parts[1]}, Destination: {parts[2]}, Max Seats: {parts[3]}, Passengers: {parts[4]}";
    }

    public static string FindBookingByID(int bookingID, string[] lines)
    {
        foreach (var line in lines)
        {
            string[] parts = line.Split('|');
            if (int.Parse(parts[0]) == bookingID)
            {
                return line;
            }
        }

        return null; 
    }
    
}