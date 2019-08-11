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
    class AuthorsControllerUnitTests
    {
        public static readonly DbContextOptions<questionCollectionContext> options
            = new DbContextOptionsBuilder<questionCollectionContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase2")
                .Options;

        public static readonly IList<Authors> authors = new List<Authors>
        {
        };

        [TestInitialize]
        public void SetupDb()
        {
            using (var context = new questionCollectionContext(options))
            {
                // populate the db
                context.Authors.Add(authors[0]);
                context.Authors.Add(authors[1]);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new questionCollectionContext(options))
            {
                // clear the db
                context.Authors.RemoveRange(context.Authors);
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestGetSuccessfully()
        {
            using (var context = new questionCollectionContext(options))
            {
                AuthorsController authorsController = new AuthorsController(context);
                ActionResult<IEnumerable<Authors>> result = await authorsController.GetAuthors();

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
                Authors authors1 = context.Authors.Where(x => x.Phrase == authors[0].Phrase).Single();
                authors1.Phrase = newPhrase;

                AuthorsController authorsController = new AuthorsController(context);
                IActionResult result = await authorsController.PutAuthors(authors1.AuthorId, authors1) as IActionResult;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }
        }

    }
}
