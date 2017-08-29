using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Model;
using WebApi.Interfaces;

namespace WebApi
{
    public class CustomersService: ICustomersService
    {
        public List<Customer> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
