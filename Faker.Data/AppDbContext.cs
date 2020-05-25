using System;
using System.Threading;
using System.Threading.Tasks;
using Faker.Data.Models;
using FizzWare.NBuilder;
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
            var generator = new RandomGenerator();



            var customers = Builder<Customer>.CreateListOfSize(500)
                                             .All()
                                                .With(c => c.ID = generator.Guid().ToString())
                                                .With(c => c.FirstName = Faker.Name.First())
                                                .With(c => c.LastName = Faker.Name.Last())
                                                .With(c => c.Address = $"{c.FirstName}.{c.LastName}@domain.com")
                                             .Build();
            
            this.Customers.AddRange(customers);

            this.SaveChanges();
        }
    }
}
