using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightServiceAPI.Data;
using FlightServiceAPI.Models;
using FlightServiceAPI.DTO;

namespace FlightServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FSContext _context;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(ILogger<FlightsController> logger, FSContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            return await _context.Flights.ToListAsync();
        }

        // GET: api/Flights/Passengers
        [HttpGet("Passengers")]
        public async Task<ActionResult<IEnumerable<FlightPassenger>>> GetBookings()
        {
            if (_context.FlightPassengers == null)
            {
                return NotFound();
            }
            return await _context.FlightPassengers.ToListAsync();
        }

        // GET: api/Flights/Passenger/5
        [HttpGet("Passenger/{id}")]
        public async Task<ActionResult<List<Flight>>> GetFlightsByPassenger(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers.Include(p => p.FlightPassengers).ThenInclude(fp => fp.Flight).FirstOrDefaultAsync(p => p.Id == id);
            var fp = passenger.FlightPassengers;
            var fDTO = new List<FlightDTO>();

            foreach (var p in fp)
            {
                var d = new FlightDTO
                {
                    Id = p.FlightId,
                    Airline = p.Flight.Airline,
                    DepartureDate = p.Flight.DepartureDate,
                    DepartureTime = p.Flight.DepartureTime,
                    ArrivalDate = p.Flight.ArrivalDate,
                    ArrivalTime = p.Flight.ArrivalTime,
                    BoardingTime = p.Flight.BoardingTime,
                    GateNumber = p.Flight.GateNumber,
                    Destination = p.Flight.Destination,
                    MaxNumberOfSeats = p.Flight.MaxNumberOfSeats
                  
                };

                fDTO.Add(d);
            }


            return Ok(fDTO);
        }


        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            var passengers = await _context.Passengers.Where(
                f => f.FlightPassengers.Where(aof => aof.FlightId == flight.Id).Any()).ToListAsync();

            var fdDTO = new FlightDetailsDTO
            {
                Id = flight.Id,
                Airline = flight.Airline,
                DepartureDate = flight.DepartureDate,
                DepartureTime = flight.DepartureTime,
                ArrivalDate = flight.ArrivalDate,
                ArrivalTime = flight.ArrivalTime,
                BoardingTime = flight.BoardingTime,
                GateNumber = flight.GateNumber,
                Destination = flight.Destination,
                MaxNumberOfSeats = flight.MaxNumberOfSeats
                //ConfirmationNumber = flight.ConfirmationNumber,
                //PassengerCount = flight.PassengerCount,
                //Passengers = passengers
            };

            return Ok(fdDTO);
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Flights/5/Passenger/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{flightId}/Passenger/{passengerId}")]
        public async Task<ActionResult<Flight>> PostPassenger(int flightId, int passengerId)
        {
            if (_context.Flights == null)
            {
                return Problem("Entity set 'FSContext.Flights'  is null.");
            }           
            var flight = await _context.Flights.FindAsync(flightId);
            var passenger = await _context.Passengers.FindAsync(passengerId);

            var fp = new FlightPassenger
            {
                FlightId = flightId,
                Flight = flight,
                PassengerId = passengerId,
                Passenger = passenger
            };

            _context.FlightPassengers.Add(fp);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(FlightDTO flightDTO)
        {
          if (_context.Flights == null)
          {
              return Problem("Entity set 'FSContext.Flights'  is null.");
          }

            var flight = new Flight()
            {
                Airline = flightDTO.Airline,
                DepartureDate = flightDTO.DepartureDate,
                DepartureTime = flightDTO.DepartureTime,
                ArrivalDate = flightDTO.ArrivalDate,
                ArrivalTime = flightDTO.ArrivalTime,
                BoardingTime = flightDTO.BoardingTime,
                GateNumber = flightDTO.GateNumber,
                Destination = flightDTO.Destination,
                MaxNumberOfSeats = flightDTO.MaxNumberOfSeats
            };

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.Id }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Flights/Passengers/5
        [HttpDelete("Passengers/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (_context.FlightPassengers == null)
            {
                return NotFound();
            }
            var flightsPassengers = await _context.FlightPassengers.FindAsync(id);
            if (flightsPassengers == null)
            {
                return NotFound();
            }

            _context.FlightPassengers.Remove(flightsPassengers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
