using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WebApi.Domain.Model;

namespace WebApi.Tests
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class CustomersRepositoryTests
    {
        private readonly List<Customer> _expected = new List<Customer>
        {
            new Customer
            {
                PassportId = "AB111111",
                FirstName = "Bohdan",
                LastName = "Kucheriavyi",
                DateOfBirth = new DateTime(1991, 09, 23),
                EyesColor = EyesColor.Green
            },
            new Customer
            {
                PassportId = "AB111112",
                FirstName = "Bob",
                LastName = "McSteven",
                DateOfBirth = new DateTime(1956, 03, 04),
                EyesColor = EyesColor.Gray
            }
        };

        [SetUp]
        public void SetUp()
        {
            //TODO: Reset db context and seed 
        }

        [Test]
        public void SelectAll_ShouldSelectAllCustomers()
        {
            //given
            var repo = new CustomersService();

            //when
            var result = repo.SelectAll().OrderBy(r => r.PassportId).ToList();

            //then
            Assert.That(result.Count, Is.EqualTo(_expected.Count));
            Assert.That(result[0].PassportId, Is.EqualTo(_expected[0].PassportId));
            Assert.That(result[1].PassportId, Is.EqualTo(_expected[1].PassportId));
        }

        [Test]
        public void GetById_ShouldRequiredCustomer_UniqueValue()
        {
            //given
            var id = "AB_12235";
            var repo = new CustomersService();

            //when            
            var result = repo.GetById(id);

            //then
            Assert.That(result.PassportId, Is.EqualTo(_expected[1].PassportId));
            Assert.That(result.DateOfBirth, Is.EqualTo(_expected[1].DateOfBirth));
            Assert.That(result.FirstName, Is.EqualTo(_expected[1].FirstName));
            Assert.That(result.LastName, Is.EqualTo(_expected[1].LastName));
            Assert.That(result.EyesColor, Is.EqualTo(_expected[1].EyesColor));
        }

        [Test]
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

            var repo = new CustomersService();

            //when        
                
            repo.Add(newCustomer);
            var result = repo.SelectAll();

            //then
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[2].PassportId, Is.EqualTo(newCustomer.PassportId));
            Assert.That(result[2].FirstName, Is.EqualTo(newCustomer.FirstName));
            Assert.That(result[2].LastName, Is.EqualTo(newCustomer.LastName));
            Assert.That(result[2].DateOfBirth, Is.EqualTo(newCustomer.DateOfBirth));
            Assert.That(result[2].EyesColor, Is.EqualTo(newCustomer.EyesColor));
        }

        [Test]
        public void Add_UpdatesExistingCustomer()
        {
            //given
            var newCustomer = new Customer
            {
                PassportId = _expected[1].PassportId,
                FirstName = "New Customer Name 1",
                DateOfBirth = new DateTime(2002,1,1)
            };

            var repo = new CustomersService();

            //when        
            repo.Add(newCustomer);
            var result = repo.SelectAll();

            //then
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[1].PassportId, Is.EqualTo(_expected[1].PassportId));
            Assert.That(result[1].FirstName, Is.EqualTo(newCustomer.FirstName));
            Assert.That(result[1].LastName, Is.EqualTo(_expected[1].LastName));
            Assert.That(result[1].DateOfBirth, Is.EqualTo(newCustomer.DateOfBirth));
            Assert.That(result[1].EyesColor, Is.EqualTo(_expected[1].EyesColor));
        }

        [Test]
        public void Remove_RemovesExistingCustomer()
        {
            //given
            var repo = new CustomersService();

            //when        
            repo.Remove(_expected[0].PassportId);
            var result = repo.SelectAll();

            //then
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PassportId, Is.EqualTo(_expected[1].PassportId));
        }

        [Test]
        public void Remove_DoNothingForNonExistingCustomer()
        {
            //given
            var repo = new CustomersService();

            //when        
            repo.Remove("non existing id");
            var result = repo.SelectAll();

            //then
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].PassportId, Is.EqualTo(_expected[0].PassportId));
            Assert.That(result[1].PassportId, Is.EqualTo(_expected[1].PassportId));
        }
    }
}
