using System;
using System.Collections.Generic;
using WebApi.Domain.Model;

namespace WebApi.Domain
{
   public static class DefaultCustomers
   {
       public static List<Customer> Expected = new List<Customer>
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
   }
}
