using System.ComponentModel.DataAnnotations.Schema;

namespace FlightServiceAPI.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        // Navigation Properties
        // JOIN entity
        public virtual ICollection<FlightPassenger> FlightPassengers { get; set; }

    }
}
