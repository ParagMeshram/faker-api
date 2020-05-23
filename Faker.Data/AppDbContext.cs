using System;
using System.Threading;
using System.Threading.Tasks;
using Faker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Faker.Data
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }

        public void Initialize()
        {
            var customer = new Customer
            {
                ID = "xyz",
                FirstName = "Parag",
                LastName = "Meshram",
                Address = "Pune"
            };

            this.Customers.Add(customer);
            this.SaveChanges();
        }
    }
}
