namespace FlightReservationSystemProject;

using System; 
using System.Linq;  //TODO; what does this do? why? 
// The System.Linq namespace provides methods like Where, Select, and ToArray for querying and manipulating data collections

// TODO: Keep names as is for data structs? 
// Data structures (CustomerAcc, Flight, Booking) 
public class CustomerAcc
{
    private int customerID;
    private string customerFirstName;
    private string customerLastName;
    private string customerPhoneNum;
    private int customerNumOfBookings;

    public static int customerIDCount = 1;

    public int CustomerID
    {
        get { return customerID; }
    }

    public string CustomerFirstName
    {
        get
        {
            return customerFirstName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name cannot be empty");
            }

            customerFirstName = value.Trim();
        }
    }

    public string CustomerLastName
    {
        get
        {
            return customerLastName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name cannot be empty.");
            }

            customerLastName = value.Trim();
        }
    }

    public string PhoneNumber
    {
        get
        {
            return customerPhoneNum;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new AggregateException("Phone number cannot be empty or null.");
            }

            customerPhoneNum = CleanPhonenumber(value);
        }
    }

    public int CustomerNumOfBookings
    {
        get
        {
            return customerNumOfBookings;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Number of bokings cannot be negative");
            }

            customerNumOfBookings = value;
        }
    }
    
    // TODO: A less complex way of doing this. Perhaps looping over phone string and just pulling digits only
    private string CleanPhonenumber(string customerphone_p)
    {
        return string.Concat(PhoneNumber.Where(char.IsDigit));
    }
    
    public void AddBookingCount()
    {
        customerNumOfBookings++;
    }

    public void RemoveBookingsCount()
    {
        if (customerNumOfBookings > 0)
        {
            customerNumOfBookings--;
        }
    }

    // Default constructor; no params. Needed for main menu object init when no user input yet given. 
    public CustomerAcc()
    {
        customerID = customerIDCount++;
        customerFirstName = string.Empty;
        customerLastName = string.Empty;
        customerPhoneNum = string.Empty;
        customerNumOfBookings = 0;
    }
    
    // Constructor for Customer account object
    public CustomerAcc( string customerFirstName_p, string customerLastName_p, string customerPhoneNum_p)
    {
        customerID = customerIDCount++;
        CustomerFirstName = customerFirstName_p;
        customerLastName = customerLastName_p;
        customerPhoneNum = customerPhoneNum_p;
        customerNumOfBookings = 0;
    }
}
