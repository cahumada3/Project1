using FlightServiceAPI.DTO;
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
        public string BoardingTime { get; set; }
        public string GateNumber { get; set; }
        public string Destination { get; set; }
        public int MaxNumberOfSeats { get; set; }

        // Not stored property
        // if there are passengers return a count or return 0
        public int PassengerCount => FlightPassengers?.Count ?? 0;

        // Navigation Properties
        // JOIN entities
        public virtual ICollection<FlightPassenger> FlightPassengers { get; set; }

        //public List<FlightPassenger> FlightPassengers { get; set; }


        public Flight() { }

        public Flight(FlightDTO dto)
        {
            this.Airline = dto.Airline;
            this.DepartureDate = dto.DepartureDate;
            this.DepartureTime = dto.DepartureTime;
            this.ArrivalDate = dto.ArrivalDate;
            this.ArrivalTime = dto.ArrivalTime;
            this.BoardingTime = dto.BoardingTime;
            this.GateNumber = dto.GateNumber;
            this.Destination = dto.Destination;
            this.MaxNumberOfSeats = dto.MaxNumberOfSeats;
            this.FlightPassengers = new List<FlightPassenger>();
        }
    }
}
