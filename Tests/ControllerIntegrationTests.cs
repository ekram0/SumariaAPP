using EFCore.App;
using EFCore.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class ControllerIntegrationTests
    {
        private readonly WebApplicationFactory<Startup> factory;
        public ControllerIntegrationTests()
        {
            factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task GetEndpointReturnsSuccessAndSomeDataFromTheDatabse()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/SamuraisSoc");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObjectList = JsonConvert.DeserializeObject<List<Samurai>>(responseString);

            Assert.AreNotEqual(0, responseObjectList.Count);
        }


    }
}
