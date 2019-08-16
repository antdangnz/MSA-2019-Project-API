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
                context.Database.EnsureCreated(); // Added this in to check if database was created properly
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

                context.Database.EnsureDeleted(); // Added to check if database will be cleared. This might fix my previous issue
                context.Dispose();
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
                Assert.AreEqual(1, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("Big Boy", result.Value.ElementAt(0).QuestionType);
                Assert.AreEqual("If fathers day is the answer", result.Value.ElementAt(0).QuestionText);

                result = await questionsController.GetQuestionsByAuthor("Woox");

                Assert.IsNotNull(result.Value);
                Assert.AreEqual(2, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("Easy as", result.Value.ElementAt(0).QuestionType);
                Assert.AreEqual("Whats the meaning of life", result.Value.ElementAt(0).QuestionText);

            }
        }


        // Not sure if these will be able to be done. InMemory documentation suggests that I can't emulate a relational database
        [TestMethod]
        public async Task TestGetQuestionInstitutionSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestionsInstitution("UoA");

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("Big Boy", result.Value.ElementAt(0).QuestionType);
                Assert.AreEqual("If fathers day is the answer", result.Value.ElementAt(0).QuestionText);

                result = await questionsController.GetQuestionsInstitution("AUT");

                Assert.IsNotNull(result.Value);
                Assert.AreEqual(2, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("Easy as", result.Value.ElementAt(0).QuestionType);
                Assert.AreEqual("Whats the meaning of life", result.Value.ElementAt(0).QuestionText);
            }
        }

        [TestMethod]
        public async Task TestGetQuestionTypeSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> 
                result = await questionsController.GetQuestionsByType("Big Boy");

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("UoA", result.Value.ElementAt(0).Institution);
                Assert.AreEqual("If fathers day is the answer", result.Value.ElementAt(0).QuestionText);


                result = await questionsController.GetQuestionsByType("Easy as");

                Assert.IsNotNull(result.Value);
                Assert.AreEqual(2, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("AUT", result.Value.ElementAt(0).Institution);
                Assert.AreEqual("Whats the meaning of life", result.Value.ElementAt(0).QuestionText);

            }
        }

        [TestMethod]
        public async Task TestGetQuestionClassSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestionsByClass("OSRS");

                Assert.IsNotNull(result.Value);
                Assert.AreEqual(2, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("Easy as", result.Value.ElementAt(0).QuestionType);
                Assert.AreEqual("AUT", result.Value.ElementAt(0).Institution);
                Assert.AreEqual("Whats the meaning of life", result.Value.ElementAt(0).QuestionText);

                result = await questionsController.GetQuestionsByClass("COMPSYS");

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Value.ElementAt(0).QuestionId);
                Assert.AreEqual("Big Boy", result.Value.ElementAt(0).QuestionType);
                Assert.AreEqual("UoA", result.Value.ElementAt(0).Institution);
                Assert.AreEqual("If fathers day is the answer", result.Value.ElementAt(0).QuestionText);

            }
        }

        // ---- Ratings Related ----

        // Cant test using InMemory since RatingsValue and RatingsForQuestion retrieves the actual values from the SQL Database.
        // Documentation suggests that InMemory either can't handle Relational Databases, or my implementation doesn't accomodate
        // its use.

        [TestMethod]
        public async Task TestGetRatingsForQuestionSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<Ratings> result = await questionsController.GetRatingForQuestion(1);

                Assert.IsNotNull(result.Value);
                Assert.AreEqual(1, result.Value.RatingId);
            }
        }

        [TestMethod]
        public async Task TestGetRatingsValueSuccessful()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<int> result = await questionsController.GetRatingValue(2);

                Assert.IsNotNull(result.Value);
                //Assert.AreEqual(4, result.Value);
            }
        }

        // Can't test with InMemory. InMemory doesn't have the capabilities to emulate a relational database??
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
