using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using QuestionCollection.Controllers;
using QuestionCollection.Model;

namespace QuestionCollectionUnitTests
{
    class RatingsControllerUnitTests
    {
        public static readonly DbContextOptions<questionCollectionContext> options
            = new DbContextOptionsBuilder<questionCollectionContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase1")
                .Options;

        public static readonly IList<Ratings> ratings = new List<Ratings>
        {

        };

        [TestInitialize]
        public void SetupDb()
        {
            using (var context = new questionCollectionContext(options))
            {
                // populate the db
                context.Ratings.Add(ratings[0]);
                context.Ratings.Add(ratings[1]);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new questionCollectionContext(options))
            {
                // clear the db
                context.Ratings.RemoveRange(context.Ratings);
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestGetSuccessfully()
        {
            using (var context = new questionCollectionContext(options))
            {
                RatingsController ratingsController = new RatingsController(context);
                ActionResult<IEnumerable<Ratings>> result = await ratingsController.GetRatings();

                Assert.IsNotNull(result);
                // i should really check to make sure the exact transcriptions are in there, but that requires an equality comparer,
                // which requires a whole nested class, thanks to C#'s lack of anonymous classes that implement interfaces
            }
        }

        // unfortunately, it can be hard to avoid test method names that are also descriptive
        //[TestMethod]
        //public async Task TestPutTranscriptionNoContentStatusCode()
        //{
        //    using (var context = new questionCollectionContext(options))
        //    {
        //        string newPhrase = "this is now a different phrase";
        //        Ratings ratings1 = context.Questions.Where(x => x.Phrase == ratings[0].Phrase).Single();
        //        ratings1.Phrase = newPhrase;

        //        RatingsController ratingsController = new RatingsController(context);
        //        IActionResult result = await ratingsController.PutRatings(ratings1.RatingId, ratings1) as IActionResult;

        //        Assert.IsNotNull(result);
        //        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        //    }
        //}

    }
}
