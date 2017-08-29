using System.Data.Entity;

namespace WebApi.Domain
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<CustomersDbContext>
    {
        protected override void Seed(CustomersDbContext context)
        {
            base.Seed(context);

            context.Customers.Add(DefaultCustomers.Expected[0]);
            context.Customers.Add(DefaultCustomers.Expected[1]);
        }
    }
}