using Employee.Domain.Model;
using Employee.Domain.Repositary.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
            : base("name=EmployeeDBConnectionString") 
        {
            Database.SetInitializer<EmployeeDbContext>(new CreateDatabaseIfNotExists<EmployeeDbContext>());
        }
        public DbSet<Model.Employee> Employees { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEngagement> ProjectEngagements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ProjectEngagementConfiguration());
        }
    }
}
