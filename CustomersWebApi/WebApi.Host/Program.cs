using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Owin.Hosting;
using WebApi.Domain;

namespace WebApi.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUri = "http://localhost:8080";
            Console.WriteLine("Initializing and seeding database...");
            Database.SetInitializer(new ApplicationDbInitializer());
            var db = new CustomersDbContext();
            int count = db.Customers.Count();
            Console.WriteLine("Initializing and seeding database with {0} customer records...", count);

            Console.WriteLine("Starting web Server...");
            using (var appp = WebApp.Start<Startup>(baseUri))
            {
                Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
                Console.ReadLine();
            }
        }

    }
}
