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
        public DbSet<FlightPassenger> FlightPassengers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<FlightPassenger>(e =>
            {
                e.HasKey(fp => fp.Id);

                e.HasOne(fp => fp.Passenger)
                .WithMany(p => p.FlightPassengers)
                .HasForeignKey(fp => fp.PassengerId);

                e.HasOne(bf => bf.Flight)
                .WithMany(f => f.FlightPassengers)
                .HasForeignKey(bf => bf.FlightId);
            });

            //builder.Entity<FlightPassenger>(e =>
            //{
            //    e.HasKey(fp => fp.Id);

            //    e.HasOne(fp => fp.Passenger)
            //    .WithMany(p => p.FlightPassengers)
            //    .HasForeignKey(fp => fp.PassengerId);

            //    e.HasOne(bf => bf.Flight)
            //    .WithMany(f => f.FlightPassengers)
            //    .HasForeignKey(bf => bf.FlightId);
            //});



        }
    }
}
