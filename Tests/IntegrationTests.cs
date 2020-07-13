using EFCore.App;
using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{

    [TestClass]
    public class IntegrationTests
    {

        [TestMethod]
        public void BizDataGetSamuraiReturnsSamurai()
        {
            //Arrange (set up builder & seed data)
            var builder = new DbContextOptionsBuilder<SamuriaContext>();
            builder.UseInMemoryDatabase("GetSamurai");
            int samuraiId = SeedWithOneSamurai(builder.Options);
            //Act (call the method)
            using (var context = new SamuriaContext(builder.Options))
            {
                var bizlogic = new BusinessLogicData(context);
                var samuraiRetrieved = bizlogic.GetSamuraiById(samuraiId);
                //Assert (check the results)
                Assert.AreEqual(samuraiId, samuraiRetrieved.Result.Id);
            };
        }

        [TestMethod]
        public void QueryResultsAreNotTracked()
        {
            //Arrange (set up builder & seed data)
            var builder = new DbContextOptionsBuilder<SamuriaContext>();
            builder.UseInMemoryDatabase("UnTrackedSamurai");
            SeedWithOneSamurai(builder.Options);
            //Act (call the method)
            using (var context = new SamuriaContext(builder.Options))
            {
                context.Samurais.ToList();
                //Assert (check the results)
                Assert.AreEqual(0, context.ChangeTracker.Entries().Count());
            };
        }

        private int SeedWithOneSamurai(DbContextOptions<SamuriaContext> options)
        {
            using (var seedcontext = new SamuriaContext(options))
            {
                var samurai = new Samurai();
                seedcontext.Samurais.Add(samurai);
                seedcontext.SaveChanges();
                return samurai.Id;
            }
        }
    }
}
