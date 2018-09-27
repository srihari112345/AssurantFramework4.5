using Employee.Domain.Model;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Employee.Domain.Repositary.Configuration
{
    class EmployeeConfiguration : EntityTypeConfiguration<Model.Employee>
    {
        public void EmployeeConfigure()
        {
            //Map Table
            this.ToTable("Employee");

            //Primary Key
            this.HasKey(k => k.EmployeeId);

            Property(x => x.EmployeeId).HasColumnName("EmployeeId");
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.BaseLocation).HasColumnName("BaseLocation").HasMaxLength(20);           
        }
    }
}
