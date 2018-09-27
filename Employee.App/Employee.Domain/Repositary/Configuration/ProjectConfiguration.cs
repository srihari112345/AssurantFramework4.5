using Employee.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Repositary.Configuration
{
    public class ProjectConfiguration: EntityTypeConfiguration<Project>
    {
        public void ProjectConfigure()
        {
            //Map Table
            ToTable("Project");

            //Primary Key
            HasKey(k => k.ProjectId);

            //Properties
            Property(p => p.ProjectId).HasColumnName("ProjectId");
            Property(p => p.Name).HasColumnName("Name");
        }
    }
}
