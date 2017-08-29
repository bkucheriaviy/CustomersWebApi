using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using WebApi.Domain;
using WebApi.Domain.Model;

namespace WebApi.Controllers
{
    public class CustomersController: ApiController
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger(typeof(CustomersController));

        private readonly CustomersDbContext _context;

        public CustomersController()
        {
            _context = new CustomersDbContext();
        }

        public async Task<Customer> Get(string id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PassportId == id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            if (!customers.Any())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customers;
        }

        public async Task<int> Get(DateTime dateOfBirth)
        {
            var customers = await _context.Customers.CountAsync(c=> c.DateOfBirth == dateOfBirth);
            return customers;
        }

        public async Task<IHttpActionResult> Post(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest($"Argument {nameof(customer)} is null");
            }
            _context.Customers.AddOrUpdate(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PassportId == id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
