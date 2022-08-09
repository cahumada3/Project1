namespace FlightServiceAPI.Models
{
    public class BookedFlight
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public virtual Passenger Passenger { get; set; }
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        public bool RoundTrip { get; set; }

    }
}
