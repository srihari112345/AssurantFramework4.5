using Employee.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Employee.Domain.Repositary.Configuration
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        // ToTable("Address");

        public AddressConfiguration()
        {
            //Map Table
            this.ToTable("Address");

            //Primary Key
            this.HasKey(x => x.AddressId);

            //Properties
            this.Property(p => p.AddressId).HasColumnName("AddressId");
            this.Property(p => p.EmployeeId).HasColumnName("EmployeeId");
            this.Property(p => p.Line1).HasColumnName("Line1");
            this.Property(p => p.Line2).HasColumnName("Line2");
            this.Property(p => p.Line3).HasColumnName("Line3");
            this.Property(p => p.City).HasColumnName("City");
            this.Property(p => p.State).HasColumnName("State");
            this.Property(p => p.Country).HasColumnName("Country");

            //Relationships         
            this.HasRequired(x => x.Employee).WithMany(x => x.Addresses)
                                             .HasForeignKey(x => x.EmployeeId);

            
        }
    }
}

