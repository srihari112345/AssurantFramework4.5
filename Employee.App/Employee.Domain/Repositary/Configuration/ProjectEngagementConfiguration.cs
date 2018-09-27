using Employee.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Repositary.Configuration
{
    class ProjectEngagementConfiguration: EntityTypeConfiguration<ProjectEngagement>
    {
        public void ProjectEngagementConfigure()
        {
            //Map Table
            ToTable("ProjectEngagement");

            //Primary Key
            HasKey(x => x.EngagementId);

            Property(p => p.EngagementId).HasColumnName("EngagementId");
            Property(p => p.EmployeeId).HasColumnName("EmployeeId");
            Property(p => p.ProjectId).HasColumnName("ProjectId");

            //Relationships
            HasRequired(p => p.Project)
                   .WithMany(p => p.ProjectEngagements)
                   .HasForeignKey(p => p.ProjectId);

            HasRequired(p => p.Employee)
                   .WithMany(p => p.ProjectEngagements)
                   .HasForeignKey(p => p.EmployeeId);
        }
    }
}
