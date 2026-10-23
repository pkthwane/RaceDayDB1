using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;
using Microsoft.AspNetCore.Authorization;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetResults()
        {
            var results = _context.Results.ToList();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetResult(int id)
        {
            var result = _context.Results.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public IActionResult AddResult(Result result)
        {
            _context.Results.Add(result);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetResult),
                new { id = result.ResultID },
                result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult UpdateResult(int id, Result updatedResult)
        {
            var result = _context.Results.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            result.RunnerID = updatedResult.RunnerID;
            result.RaceID = updatedResult.RaceID;
            result.FinishTime = updatedResult.FinishTime;
            result.Position = updatedResult.Position;

            _context.SaveChanges();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult DeleteResult(int id)
        {
            var result = _context.Results.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            _context.SaveChanges();

            return Ok("Result deleted successfully.");
        }
    }
}