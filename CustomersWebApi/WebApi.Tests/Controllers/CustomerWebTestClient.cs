using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApi.Domain.Model;

namespace WebApi.Tests.Controllers
{
    public class CustomerWebTestClient
    {
        readonly string _hostUri;
        public CustomerWebTestClient(string hostUri)
        {
            _hostUri = hostUri;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            HttpResponseMessage response;
            using (var client = new HttpClient { BaseAddress = new Uri(_hostUri) })
            {
                response = client.GetAsync("api/customers").Result;
            }
            var result = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            return result;
        }

        public Customer GetCustomer(string id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient { BaseAddress = new Uri(_hostUri) })
            {
                response = client.GetAsync(($"api/customers/{id}")).Result;
            }
            var result = response.Content.ReadAsAsync<Customer>().Result;
            return result;
        }

        public HttpStatusCode PostCustomer(Customer customer)
        {
            HttpResponseMessage response;
            
            using (var client = new HttpClient { BaseAddress = new Uri(_hostUri) })
            {
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync(client.BaseAddress, customer).Result;
            }
            return response.StatusCode;
        }

        public HttpStatusCode DeleteCustomer(string id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient { BaseAddress = new Uri(_hostUri) })
            {
                response = client.DeleteAsync(new Uri(client.BaseAddress, id)).Result;
            }
            return response.StatusCode;
        }
    }
}