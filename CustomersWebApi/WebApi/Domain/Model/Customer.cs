using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Model
{
    public class Customer
    {
        [Key]
        public string PassportId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public EyesColor EyesColor { get; set; }
    }
}
