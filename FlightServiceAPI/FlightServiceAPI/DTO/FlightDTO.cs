namespace FlightServiceAPI.DTO
{
    public class FlightDTO
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

        


    }
}
