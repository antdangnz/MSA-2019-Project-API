using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using QuestionCollection.Controllers;
using QuestionCollection.Model;
using System.Diagnostics;
using System;

namespace QuestionCollectionUnitTests
{
    [TestClass]
    public class RatingsControllerUnitTests
    {
        public static readonly DbContextOptions<questionCollectionContext> options
            = new DbContextOptionsBuilder<questionCollectionContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase1")
                .Options;

        public static readonly IList<Ratings> ratings = new List<Ratings>
        {
            new Ratings()
            {
                RatingId = 1,
                QuestionId = 2,
                Rating = 69,
                RatingDescription = "This brings great joy"
            },
            new Ratings()
            {
                RatingId = 2,
                QuestionId = 3,
                Rating = 4,
                RatingDescription = "This does not bring joy"
            }
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
        public async Task TestGetRatingsByQuestionSuccessfully()
        {
            using (var context = new questionCollectionContext(options))
            {
                RatingsController ratingsController = new RatingsController(context);
                ActionResult<Ratings> result = await ratingsController.GetRatingsByQuestion(2);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Value);

                // Same test
                Assert.IsTrue(result.Value.RatingId == 1);
                Assert.AreEqual(1, result.Value.RatingId);

                Assert.IsTrue(result.Value.RatingDescription == "This brings great joy");
                Assert.IsTrue(result.Value.Rating == 69);
                Assert.IsFalse(result.Value.Rating == 4);
                Assert.IsFalse(result.Value.RatingId == 123);

                result = await ratingsController.GetRatingsByQuestion(3);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Value);
                Assert.IsTrue(result.Value.RatingId == 2);
                Assert.IsTrue(result.Value.RatingDescription == "This does not bring joy");
                Assert.IsTrue(result.Value.Rating == 4);
                Assert.IsFalse(result.Value.Rating == 69);
                Assert.IsFalse(result.Value.RatingId == 456);
            }
        }

        [TestMethod]
        public async Task TestGetRatingsByQuestionFail()
        {
            using (var context = new questionCollectionContext(options))
            {
                RatingsController ratingsController = new RatingsController(context);
                ActionResult<Ratings> result = await ratingsController.GetRatingsByQuestion(1);

                //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out)); Trace.WriteLine("Hello World");
                //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out)); Trace.WriteLine(result.Value);
                Assert.IsNull(result.Value);

                result = await ratingsController.GetRatingsByQuestion(66);

                Assert.IsNull(result.Value);
            }
        }

    }
}
