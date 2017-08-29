using System;
using System.Data.Entity;
using WebApi.Domain.Model;

namespace WebApi.Domain
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<CustomersDbContext>
    {
        protected override void Seed(CustomersDbContext context)
        {
            base.Seed(context);

            context.Customers.Add(new Customer
            {
                PassportId = "AB111111",
                FirstName = "Bohdan",
                LastName = "Kucheriavyi",
                DateOfBirth = new DateTime(1991, 09, 23),
                EyesColor = EyesColor.Green
            });
            context.Customers.Add(new Customer
            {
                PassportId = "AB111112",
                FirstName = "Bob",
                LastName = "McSteven",
                DateOfBirth = new DateTime(1956, 03, 04),
                EyesColor = EyesColor.Gray
            });
        }
    }
}