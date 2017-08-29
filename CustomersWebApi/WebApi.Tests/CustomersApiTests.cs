using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Owin.Hosting;
using NUnit.Framework;
using WebApi.Domain;
using WebApi.Domain.Model;
using WebApi.Host;
using WebApi.Tests.Controllers;

namespace WebApi.Tests
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class CustomersApiTests
    {
        private IDisposable _app;
        private readonly string _host = "http://localhost:8080";

        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
            var db = new CustomersDbContext();
            int count = db.Customers.Count();
            
            _app = WebApp.Start<Startup>(_host);
        }

        [TearDown]
        public void Teardown()
        {
            _app?.Dispose();
        }

        [Test]
        public void GetWithoutParameters_ShouldSelectAllCustomers()
        {
            //given
            var client = new CustomerWebTestClient(_host);

            //when
            var result = client.GetCustomers().OrderBy(c=> c.PassportId).ToList();

            //then
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].PassportId, Is.EqualTo(DefaultCustomers.Expected[0].PassportId));
            Assert.That(result[1].PassportId, Is.EqualTo(DefaultCustomers.Expected[1].PassportId));
        }

        [Test]
        public void GetById_ShouldRequiredCustomer_UniqueValue()
        {
            //given
            var client = new CustomerWebTestClient(_host);

            //when            
            var result = client.GetCustomer(DefaultCustomers.Expected[1].PassportId);

            //then
            Assert.That(result.PassportId, Is.EqualTo(DefaultCustomers.Expected[1].PassportId));
            Assert.That(result.DateOfBirth, Is.EqualTo(DefaultCustomers.Expected[1].DateOfBirth));
            Assert.That(result.FirstName, Is.EqualTo(DefaultCustomers.Expected[1].FirstName));
            Assert.That(result.LastName, Is.EqualTo(DefaultCustomers.Expected[1].LastName));
            Assert.That(result.EyesColor, Is.EqualTo(DefaultCustomers.Expected[1].EyesColor));
        }

        [Test]
        [Ignore("Test client formatting problem")]
        public void Add_AddingNonExistingCustomer()
        {
            //given
            var newCustomer = new Customer
            {
                PassportId = "for sure new id",
                FirstName = "New Customer Name 1",
                LastName = "new Customer LastName 1",
                DateOfBirth = new DateTime(2000,1,1),
                EyesColor = EyesColor.Black
            };

            var client = new CustomerWebTestClient(_host);

            //when        

            var resultCode = client.PostCustomer(newCustomer);
            var customers = client.GetCustomers().OrderBy(c=> c.PassportId).ToList();

            //then
            Assert.That(resultCode, Is.EqualTo(200));
            Assert.That(customers.Count, Is.EqualTo(3));
            Assert.That(customers[2].PassportId, Is.EqualTo(newCustomer.PassportId));
            Assert.That(customers[2].FirstName, Is.EqualTo(newCustomer.FirstName));
            Assert.That(customers[2].LastName, Is.EqualTo(newCustomer.LastName));
            Assert.That(customers[2].DateOfBirth, Is.EqualTo(newCustomer.DateOfBirth));
            Assert.That(customers[2].EyesColor, Is.EqualTo(newCustomer.EyesColor));
        }

        [Test]
        [Ignore("Test client formatting problem")]
        public void Add_UpdatesExistingCustomer()
        {
            //given
            var newCustomer = new Customer
            {
                PassportId = DefaultCustomers.Expected[1].PassportId,
                FirstName = "New Customer Name 1",
                DateOfBirth = new DateTime(2002,1,1)
            };

            var client = new CustomerWebTestClient(_host);

            //when        
            var responseCode = client.PostCustomer(newCustomer);
            var result = client.GetCustomers().OrderBy(c => c.PassportId).ToList();

            //then
            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[1].PassportId, Is.EqualTo(DefaultCustomers.Expected[1].PassportId));
            Assert.That(result[1].FirstName, Is.EqualTo(newCustomer.FirstName));
            Assert.That(result[1].LastName, Is.EqualTo(DefaultCustomers.Expected[1].LastName));
            Assert.That(result[1].DateOfBirth, Is.EqualTo(newCustomer.DateOfBirth));
            Assert.That(result[1].EyesColor, Is.EqualTo(DefaultCustomers.Expected[1].EyesColor));
        }

        [Test]
        [Ignore("Test client formatting problem")]
        public void Remove_RemovesExistingCustomer()
        {
            //given
            var client = new CustomerWebTestClient(_host);

            //when        
            var responseCode = client.DeleteCustomer(DefaultCustomers.Expected[0].PassportId);
            var result = client.GetCustomers().OrderBy(c => c.PassportId).ToList(); 

            //then
            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PassportId, Is.EqualTo(DefaultCustomers.Expected[1].PassportId));
        }

        [Test]
        [Ignore("Test client formatting problem")]
        public void Remove_DoNothingForNonExistingCustomer()
        {
            //given
            var client = new CustomerWebTestClient(_host);

            //when        
            var responseCode = client.DeleteCustomer("non existing id");
            var result = client.GetCustomers().OrderBy(c => c.PassportId).ToList();

            //then
            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].PassportId, Is.EqualTo(DefaultCustomers.Expected[0].PassportId));
            Assert.That(result[1].PassportId, Is.EqualTo(DefaultCustomers.Expected[1].PassportId));
        }
    }
}
