using System.Data.Entity;
using WebApi.Domain.Model;

namespace WebApi.Domain
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext() : base("CustomersDatabase")
        {
        }

        public IDbSet<Customer> Customers { get; set; }
    }
}
