//using System;
//using System.Collections.Generic;
//using System.Net.Http;

//namespace WebApi.Tests.Controllers
//{
//    public class CustomerWebClient
//    {
//        string _hostUri;
//        public CustomerWebClient(string hostUri)
//        {
//            _hostUri = hostUri;
//        }

//        public IEnumerable<Customer> GetCompanies()
//        {
//            HttpResponseMessage response;
//            using (var client = new HttpClient { BaseAddress = new Uri(_hostUri) })
//            {
//                response = client.GetAsync("api/customers").Result;
//            }
//            var result = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
//            return result;
//        }

//        public Customer GetCompany(int id)
//        {
//            HttpResponseMessage response;
//            using (var client = CreateClient())
//            {
//                response = client.GetAsync(
//                    new Uri(client.BaseAddress, id.ToString())).Result;
//            }
//            var result = response.Content.ReadAsAsync<Customer>().Result;
//            return result;
//        }

//        public System.Net.HttpStatusCode AddCompany(Customer company)
//        {
//            HttpResponseMessage response;
//            using (var client = CreateClient())
//            {
//                response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
//            }
//            return response.StatusCode;
//        }


//        public System.Net.HttpStatusCode UpdateCompany(Customer company)
//        {
//            HttpResponseMessage response;
//            using (var client = CreateClient())
//            {
//                response = client.PutAsJsonAsync(client.BaseAddress, company).Result;
//            }
//            return response.StatusCode;
//        }


//        public System.Net.HttpStatusCode DeleteCompany(int id)
//        {
//            HttpResponseMessage response;
//            using (var client = CreateClient())
//            {
//                response = client.DeleteAsync(
//                    new Uri(client.BaseAddress, id.ToString())).Result;
//            }
//            return response.StatusCode;
//        }
//    }
//}