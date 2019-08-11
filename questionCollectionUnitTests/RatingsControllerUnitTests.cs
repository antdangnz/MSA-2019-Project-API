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
    class RatingsControllerUnitTests
    {
        public static readonly DbContextOptions<questionCollectionContext> options
            = new DbContextOptionsBuilder<questionCollectionContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;
    }
}
