using EFCore.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFCore.Domain;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    [TestClass]
    public class InMemoryTest
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDataBase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamuraiIntoDataBase");

            using SamuriaContext context = new SamuriaContext(builder.Options);
            var samurai = new Samurai();
            context.Samurais.Add(samurai);
            Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
        }
    }
}
