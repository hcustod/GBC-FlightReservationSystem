namespace FlightReservationSystemProject;


public class Flight
{
    private int flightNum;
    private string flightOrigin;
    private string flightDestination;
    private int flightMaxSeats;
    private int flightPasangerCount;

    public int FlightNum
    {
        get
        {
            return flightNum;
        }
    }

    public string FlightOrigin
    {
        get
        {
            return flightOrigin;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Origin cannot be empty.");
            }
            flightOrigin = value.Trim();
        }
    }

    public string FlightDestination
    {
        get
        {
            return flightDestination;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Destination cannot be empty");
            }

            flightDestination = value.Trim();
        }
    }

    public int FlightMaxSeats
    {
        get
        {
            return flightMaxSeats;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Max seats must be greater than 0.");
            }

            flightMaxSeats = value;
        }
    }

    public int CountPassangers
    {
        get
        {
            return flightPasangerCount;
        }
    }
    
    public bool AddPassangerToFlight()
    {
        if (flightPasangerCount < FlightMaxSeats)
        {
            flightPasangerCount++;
            return true;
        }
        return false;
    }

    public bool RemovePassangerFromFlight()
    {
        if (flightPasangerCount > 0)
        {
            flightPasangerCount--;
            return true;
        }
        return false; 
    }

    public Flight(int flightNum, string flightOrigin, string flightDestination, int flightMaxSeats)
    {
        this.flightNum = flightNum;
        FlightOrigin = flightOrigin;
        FlightDestination = flightDestination;
        FlightMaxSeats = flightMaxSeats;
        flightPasangerCount = 0;
    }
    
}