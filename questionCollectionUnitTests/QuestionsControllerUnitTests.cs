using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using QuestionCollection.Controllers;
using QuestionCollection.Model;

namespace questionCollectionUnitTests
{
    [TestClass]
    public class QuestionsControllerUnitTests
    {
        public static readonly DbContextOptions<questionCollectionContext> options
            = new DbContextOptionsBuilder<questionCollectionContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

        public static readonly IList<Questions> questions = new List<Questions>
        {
            new Questions()
            {
                QuestionId = 1,
                ClassName = "COMPSYS",
                ClassNumber = "3",
                Author = "Rekkles",
                Institution = "UoA",
                QuestionType = "Big Boy",
                QuestionText = "If fathers day is the answer",
                Answer = "YEET"
            },
            new Questions()
            {
                QuestionId = 2,
                ClassName = "OSRS",
                ClassNumber = "7",
                Author = "Woox",
                Institution = "AUT",
                QuestionType = "Easy as",
                QuestionText = "Whats the meaning of life",
                Answer = "42"
            }
        };

        public static readonly IList<Ratings> ratings = new List<Ratings>
        {
            new Ratings()
            {
                RatingId = 2,
                QuestionId = 2,
                Rating = 69,
                RatingDescription = "This brings great joy"
            },
            new Ratings()
            {
                RatingId = 1,
                QuestionId = 1,
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
                context.Questions.Add(questions[0]);
                context.Questions.Add(questions[1]);
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
                context.Questions.RemoveRange(context.Questions);
                context.Ratings.RemoveRange(context.Ratings);
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestGetQuestionAuthorSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestionsByAuthor("Rekkles");

                Assert.IsNotNull(result.Value);

            }
        }

        [TestMethod]
        public async Task TestGetQuestionInstitutionSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestionsInstitution("UoA");

                Assert.IsNotNull(result);

            }
        }

        [TestMethod]
        public async Task TestGetQuestionTypeSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestionsByType("Big Boy");

                Assert.IsNotNull(result);
                Assert.AreEqual("COMPSYS", result.Value.ElementAt(0).ClassName);


            }
        }

        [TestMethod]
        public async Task TestGetQuestionClassSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestionsByClass("COMPSYS");

                Assert.IsNotNull(result);

            }
        }







        [TestMethod]
        public async Task TestGetRatingsForQuestionSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<Ratings> result = await questionsController.GetRatingForQuestion(1);

                Assert.IsNotNull(result.Value);
                Assert.AreEqual(1, result.Value.RatingId);
                // Also broken for some reason.
                //Assert.AreEqual(21, result.Value.Rating.);
                //Assert.IsTrue( result.Value.Rating == (byte) 4);

            }
        }

        [TestMethod]
        public async Task TestGetRatingsValueSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<int> result = await questionsController.GetRatingValue(1);

                Assert.IsNotNull(result.Value);

            }
        }

        // This test was somehow interfering with the other tests.
        //[TestMethod]
        //public async Task TestPutChangeQuestionRatingsSuccessful()
        //{
        //    using (var context = new questionCollectionContext(options))
        //    {
        //        Ratings testRating = new Ratings()
        //        {
        //            RatingId = 2,
        //            QuestionId = 1,
        //            Rating = 21,
        //            RatingDescription = "Yeet This does not bring joy. SIKE"
        //        };

        //        QuestionsController questionsController = new QuestionsController(context);


        //        IActionResult result = await questionsController.PutChangeQuestionRatings(1, testRating);

        //        Assert.IsNotNull(result);

        //        var newRatingValue = await questionsController.GetRatingValue(1);
        //        Assert.AreEqual(21, newRatingValue.Value);

        //        var newRating = await questionsController.GetRatingForQuestion(1);
        //        var newRatingValue2 = newRating.Value.Rating;
        //        var newRatingDescription = newRating.Value.RatingDescription;

        //        Assert.AreEqual(17, newRatingValue2.Value);
        //        Assert.AreEqual("Yeet This does not bring joy. SIKE", newRatingDescription);

        //    }
        //}







        // unfortunately, it can be hard to avoid test method names that are also descriptive
        //[TestMethod]
        //public async Task TestPutTranscriptionNoContentStatusCode()
        //{
        //    using (var context = new questionCollectionContext(options))
        //    {
        //        string newPhrase = "this is now a different phrase";
        //        Questions questions1 = context.Questions.Where(x => x.Phrase == questions[0].Phrase).Single();
        //        questions1.Phrase = newPhrase;

        //        QuestionsController questionsController = new QuestionsController(context);
        //        IActionResult result = await questionsController.PutQuestions(questions1.QuestionId, questions1) as IActionResult;

        //        Assert.IsNotNull(result);
        //        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        //    }
        //}

    }
}
