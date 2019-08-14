using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionCollection.Model;

namespace QuestionCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly questionCollectionContext _context;

        public QuestionsController(questionCollectionContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
            _context.Questions.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestions", new { id = questions.QuestionId }, questions);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questions>> DeleteQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return questions;
        }


        /* 
         Search Section
             */

        // GET: api/Questions/
        [HttpGet("author/{author}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestionsByAuthor(string author)
        {
            //var questions = await _context.Questions.FirstOrDefaultAsync(a => a.Author == author);

            var questions = await _context.Questions.Where(q => q.Author == author).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // GET: api/Questions/5
        [HttpGet("institution/{insti}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestionsInstitution(string insti)
        {
            //var questions = await _context.Questions.FindAsync(id);
            var questions = await _context.Questions.Where(q => q.Institution == insti).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // GET: api/Questions/5
        [HttpGet("type/{questiontype}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestionsByType(string questiontype)
        {
            var questions = await _context.Questions.Where(q => q.QuestionType == questiontype).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // GET: api/Questions/5
        [HttpGet("class/{classname}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestionsByClass(string classname)
        {
            var questions = await _context.Questions.Where(q => q.ClassName == classname).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }


        /*
         Ratings Section
             */

            // Currently not working atm.
        // PUT: api/Questions/5
        [HttpPut("addrating/{id}")]
        public async Task<IActionResult> AddRatingToQuestion(int id, [FromBody] Ratings ratings)
        {
            //if (id != questions.QuestionId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(questions).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!QuestionsExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            Ratings rating = new Ratings();
            rating = ratings;

            System.Diagnostics.Debug.WriteLine(rating);

            return NoContent();
        }



        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
