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
    public class PassengersController : ControllerBase
    {
        private readonly FSContext _context;
        private readonly ILogger<PassengersController> _logger;

        public PassengersController(ILogger<PassengersController> logger, FSContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Passengers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengers()
        {
          if (_context.Passengers == null)
          {
              return NotFound();
          }
            return await _context.Passengers.ToListAsync();
        }

        // GET: api/Passengers/Flight/5
        [HttpGet("Flight/{id}")]
        public async Task<ActionResult<List<PassengerDTO>>> GetPassengersByFlight(int id)
        {
            if (_context.Passengers == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.Include(f => f.FlightPassengers).ThenInclude(fp => fp.Passenger).FirstOrDefaultAsync(f => f.Id == id);
            var fp = flight.FlightPassengers;
            var pDTO = new List<PassengerDTO>();
            foreach(var f in fp)
            {
                var d = new PassengerDTO
                {
                    Id = f.PassengerId,
                    FirstName = f.Passenger.FirstName,
                    LastName = f.Passenger.LastName,
                    Email = f.Passenger.Email
                };

                pDTO.Add(d);
            }

            return Ok(pDTO);
        }

        // GET: api/Passengers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
          if (_context.Passengers == null)
          {
              return NotFound();
          }
            var passenger = await _context.Passengers.FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        // PUT: api/Passengers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(int id, PassengerDTO passengerDTO)
        {
            if (id != passengerDTO.Id)
            {
                return BadRequest();
            }

            if(passengerDTO != null)
            {
                passengerDTO.Id = id;

                var passenger = await _context.Passengers.FindAsync(id);
                passenger.FirstName = passengerDTO.FirstName;
                passenger.LastName = passengerDTO.LastName;
                passenger.Email = passengerDTO.Email;

                _context.Entry(passenger).State = EntityState.Modified;
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to update a passenger.");

                if (!PassengerExists(id))
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

        // POST: api/Passengers/5/Flight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{passengerId}/Flight/{flightId}")]
        public async Task<ActionResult<Passenger>> PostPassenger(int passengerId, int flightId)
        {
            if (_context.Passengers == null)
            {
                return Problem("Entity set 'FSContext.Passengers'  is null.");
            }
            var passenger = await _context.Passengers.FindAsync(passengerId);
            var flight = await _context.Flights.FindAsync(flightId);

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

        // POST: api/Passengers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(PassengerDTO passengerDTO)
        {
          if (_context.Passengers == null)
          {
              return Problem("Entity set 'FSContext.Passengers'  is null.");
          }

            var passenger = new Passenger()
            {
                FirstName = passengerDTO.FirstName,
                LastName = passengerDTO.LastName,
                Email = passengerDTO.Email,
                FlightPassengers = new List<FlightPassenger>()
            };

            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassenger", new { id = passenger.Id }, passenger);
        }

        // DELETE: api/Passengers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            if (_context.Passengers == null)
            {
                return NotFound();
            }
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerExists(int id)
        {
            return (_context.Passengers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
