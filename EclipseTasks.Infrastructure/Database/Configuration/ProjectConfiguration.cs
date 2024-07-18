using EclipseTasks.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Infrastructure.Database.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
               .ToTable("project");

            builder
                .Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100);

            builder
                .Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(255);
        }
    }
}
