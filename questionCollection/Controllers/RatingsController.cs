using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questionCollection.Model;

namespace questionCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly questionCollectionContext _context;

        public RatingsController(questionCollectionContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ratings>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ratings>> GetRatings(int id)
        {
            var ratings = await _context.Ratings.FindAsync(id);

            if (ratings == null)
            {
                return NotFound();
            }

            return ratings;
        }

        // PUT: api/Ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatings(int id, Ratings ratings)
        {
            if (id != ratings.RatingId)
            {
                return BadRequest();
            }

            _context.Entry(ratings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingsExists(id))
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

        // POST: api/Ratings
        [HttpPost]
        public async Task<ActionResult<Ratings>> PostRatings(Ratings ratings)
        {
            _context.Ratings.Add(ratings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatings", new { id = ratings.RatingId }, ratings);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ratings>> DeleteRatings(int id)
        {
            var ratings = await _context.Ratings.FindAsync(id);
            if (ratings == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(ratings);
            await _context.SaveChangesAsync();

            return ratings;
        }

        private bool RatingsExists(int id)
        {
            return _context.Ratings.Any(e => e.RatingId == id);
        }
    }
}
