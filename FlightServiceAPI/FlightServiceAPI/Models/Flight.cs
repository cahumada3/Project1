namespace FlightServiceAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public int BoardingTime { get; set; }
        public string GateNumber { get; set; }
        public string Destination { get; set; }
        public int MaxNumberOfSeats { get; set; }

        // Not stored property
        // if there are passengers return a count or return 0
        public int PassengerCount => Passengers?.Count ?? 0;

        // Navigation Properties
        // JOIN entities
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<BookedFlight> BookedFlights { get; set; }
    }
}
