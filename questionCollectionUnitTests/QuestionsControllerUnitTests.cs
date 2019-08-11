using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using questionCollection.Controllers;
using questionCollection.Model;

namespace questionCollectionUnitTests
{
    [TestClass]
    class QuestionsControllerUnitTests
    {
        public static readonly DbContextOptions<questionCollectionContext> options
            = new DbContextOptionsBuilder<questionCollectionContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

        public static readonly IList<Questions> questions = new List<Questions>
        {
            new Questions()
            {
                Phrase = "That's like calling"
            },
            new Questions()
            {
                Phrase = "your peanut butter sandwich"
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
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestGetSuccessfully()
        {
            using (var context = new questionCollectionContext(options))
            {
                QuestionsController questionsController = new QuestionsController(context);
                ActionResult<IEnumerable<Questions>> result = await questionsController.GetQuestions();

                Assert.IsNotNull(result);
                // i should really check to make sure the exact transcriptions are in there, but that requires an equality comparer,
                // which requires a whole nested class, thanks to C#'s lack of anonymous classes that implement interfaces
            }
        }

        // unfortunately, it can be hard to avoid test method names that are also descriptive
        [TestMethod]
        public async Task TestPutTranscriptionNoContentStatusCode()
        {
            using (var context = new questionCollectionContext(options))
            {
                string newPhrase = "this is now a different phrase";
                Questions questions1 = context.Questions.Where(x => x.Phrase == questions[0].Phrase).Single();
                questions1.Phrase = newPhrase;

                QuestionsController questionsController = new QuestionsController(context);
                IActionResult result = await questionsController.PutQuestions(questions1.QuestionId, questions1) as IActionResult;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }
        }

    }
}
