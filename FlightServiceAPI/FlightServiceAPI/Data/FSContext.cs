using FlightServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightServiceAPI.Data
{
    public class FSContext : DbContext
    {
        //Constructor - to wire it to program.cs
        public FSContext(DbContextOptions<FSContext> options) : base(options)
        {

        }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<BookedFlight>(e =>
            {
                e.HasKey(bf => bf.Id);

                e.HasOne(bf => bf.Passenger)
                .WithMany(p => p.BookedFlights)
                .HasForeignKey(bf => bf.PassengerId);

                e.HasOne(bf => bf.Flight)
                .WithMany(f => f.BookedFlights)
                .HasForeignKey(bf => bf.FlightId);
            });
        }
    }
}
