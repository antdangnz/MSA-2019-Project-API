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

        // GET: api/author/Anthony
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

        // GET: api/institution/UOA
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

        // GET: api/type/Exam
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

        // GET: api/class/COMPSYS
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
            
        [HttpGet("RatingValue/{id}")]
        public async Task<ActionResult<int>> GetRatingValue(int id)
        {
            //var questions = await _context.Questions.Where(q => q.ClassName == classname).ToListAsync();
            questionCollectionContext tempContext = new questionCollectionContext();
            RatingsController ratingsController = new RatingsController(tempContext);

            var questionRating = await ratingsController.GetRatingsByQuestion(id);
            var questionRatingValue = questionRating.Value.Rating;

            if (questionRatingValue == null)
            {
                return NotFound();
            }

            return questionRatingValue;
        }

        [HttpGet("Rating/{id}")]
        public async Task<ActionResult<Ratings>> GetRatingForQuestion(int id)
        {
            //var questions = await _context.Questions.Where(q => q.ClassName == classname).ToListAsync();
            questionCollectionContext tempContext = new questionCollectionContext();
            RatingsController ratingsController = new RatingsController(tempContext);

            var questionRating = await ratingsController.GetRatingsByQuestion(id);

            if (questionRating == null)
            {
                return NotFound();
            }

            return questionRating;
        }


        [HttpPut("ChangeRating/{id}")]
        public async Task<IActionResult> PutChangeQuestionRatings(int id, Ratings ratings)
        {
            questionCollectionContext tempContext = new questionCollectionContext();
            RatingsController ratingsController = new RatingsController(tempContext);

            var originalRating = await ratingsController.GetRatingsByQuestion(id);
            var originalRatingId = originalRating.Value.RatingId;
            //Ratings newRatings = new Ratings()
            //{
            //    RatingId = originalRatingId,
            //    QuestionId = ratings.QuestionId,
            //    Rating = ratings.Rating,
            //    RatingDescription = ratings.RatingDescription,
            //    DateCreated = originalRating.Value.DateCreated,
            //    Question = question.Value
            //};

            Ratings newRatings = originalRating.Value;

            //newRatings.RatingId = originalRatingId;
            //newRatings.QuestionId = id;
            newRatings.Rating = ratings.Rating;
            newRatings.RatingDescription = ratings.RatingDescription;
            //newRatings.DateCreated = originalRating.Value.DateCreated;


            await ratingsController.PutRatings(originalRatingId, newRatings);

            //_context.Entry(/*question*/).State = EntityState.Modified;

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



        // Currently not working atm. Can't seem to add any ratings to any question
        //PUT: api/Questions/5
        //[HttpPut("addrating/{id}")]
        //public async Task<IActionResult> AddRatingToQuestion(int id, [FromBody] Ratings ratings)
        //{
        //    questionCollectionContext tempContext = new questionCollectionContext();
        //    RatingsController ratingsController = new RatingsController(tempContext);

        //    //Task addRating = Task.Run(async () =>
        //    //{

        //    var yeet = await this.GetQuestions(id);
        //    //if (questions == null)
        //    //{
        //    //return NotFound();
        //    //}
        //    Ratings newRating = new Ratings()
        //    {
        //        RatingId = ratings.RatingId,
        //        QuestionId = ratings.QuestionId,
        //        Rating = ratings.Rating,
        //        RatingDescription = ratings.RatingDescription,
        //        DateCreated = ratings.DateCreated,
        //        Question = yeet.Value
        //    };
        //    System.Diagnostics.Debug.WriteLine("yeeeeeeeeeeeeeeeeeeeeeet");
        //    System.Diagnostics.Debug.WriteLine(newRating.QuestionId);

        //    //var questions = await _context.Questions.FindAsync(id);
        //    var questions = await this.GetQuestions(id);
        //    System.Diagnostics.Debug.WriteLine(questions.Value.QuestionId);
        //    questions.Value.Ratings.Add(newRating);
        //    var wow = questions.Value;

        //    System.Diagnostics.Debug.WriteLine("2223323144214142412");
        //    //await ratingsController.PostRatings(newRating);

        //    //_context.Entry(questions).State = EntityState.Modified;
        //    //return questions;
        //    //});

        //    try
        //    {
        //        //await _context.SaveChangesAsync();
        //        await this.PutQuestions(id, questions.Value);
        //        System.Diagnostics.Debug.WriteLine("yeedgdagdagdagfdagdaga");
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QuestionsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}



        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
