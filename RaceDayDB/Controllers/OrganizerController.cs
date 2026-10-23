using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public OrganizerController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrganizers()
        {
            var organizers = _context.Organizers
                .Select(o => new
                {
                    o.OrgID,
                    o.Name,
                    o.ContactInfo,
                    o.Email
                })
                .ToList();

            return Ok(organizers);
        }
    }
}