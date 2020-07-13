using EFCore.App;
using EFCore.Data;
using EFCore.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class InMemoryAttemptControllerIntegrationTests
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public InMemoryAttemptControllerIntegrationTests()
        {
            _factory = new WebApplicationFactory<Startup>();
        }



        [TestMethod]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange

            // var client = _factory.CreateClient();
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var serviceDescriptors = services.Where(descriptor => (descriptor.ServiceType 
                    == typeof(DbContextOptions)) | (descriptor.ServiceType == typeof(SamuriaContext))).ToList();

                    foreach (var item in serviceDescriptors)
                    {
                        services.Remove(item);
                    }

                    services.AddDbContext<SamuriaContext>(opt =>
                   opt.UseInMemoryDatabase("ControllerGet"));
                });
            })
             .CreateClient();

            // Act
            var response = await client.GetAsync("/api/Samurais");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<List<Samurai>>(responseString);

            Assert.AreNotEqual(0, responseObject.Count);
        }

        [TestMethod]
        public async Task Post_CanInsertIntoDatabase()
        {
            // Arrange
            var client = _factory.CreateClient();
            var json = JsonConvert.SerializeObject(new Samurai { Name = "xyz" });//, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.Default, "application/json");
            //// Act
            var response = await client.PostAsync("/api/SamuraisSoc", httpContent);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<PostResponse>(responseString);

            Assert.IsTrue(responseObject.id > 0);
        }
        private struct PostResponse
        {
            public int id;
            public Samurai samurai;
        }
    }
}

