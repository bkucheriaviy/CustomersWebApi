using System.Collections.Generic;
using WebApi.Domain.Model;

namespace WebApi.Interfaces
{
    public interface ICustomersService
    {
        List<Customer> SelectAll();
        Customer GetById(string id);
        void Add(Customer customer);
        void Remove(string id);
    }
}