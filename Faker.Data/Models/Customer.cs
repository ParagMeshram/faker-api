using System;
using System.ComponentModel.DataAnnotations;

namespace Faker.Data.Models
{
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
