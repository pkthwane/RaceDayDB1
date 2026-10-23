using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RegistrationController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRegistrations()
        {
            var registrations = _context.Registrations.ToList();
            return Ok(registrations);
        }

        [HttpGet("{id}")]
        public IActionResult GetRegistration(int id)
        {
            var registration = _context.Registrations.Find(id);

            if (registration == null)
            {
                return NotFound();
            }

            return Ok(registration);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddRegistration(Registration registration)
        {
            _context.Registrations.Add(registration);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetRegistration),
                new { id = registration.RegID },
                registration);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRegistration(int id, Registration updatedRegistration)
        {
            var registration = _context.Registrations.Find(id);

            if (registration == null)
            {
                return NotFound();
            }

            registration.RunnerID = updatedRegistration.RunnerID;
            registration.RaceID = updatedRegistration.RaceID;
            registration.CategoryID = updatedRegistration.CategoryID;
            registration.PaymentStatus = updatedRegistration.PaymentStatus;

            _context.SaveChanges();

            return Ok(registration);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRegistration(int id)
        {
            var registration = _context.Registrations.Find(id);

            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            _context.SaveChanges();

            return Ok("Registration deleted successfully.");
        }
    }
}
