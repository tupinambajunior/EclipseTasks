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
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<Core.Entities.ProjectTask>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProjectTask> builder)
        {
            builder
               .ToTable("project_task");

            builder
                .Property(x => x.IdProject)
                .HasColumnName("id_project")
                .HasMaxLength(100);

            builder
                .Property(x => x.IdUser)
                .HasColumnName("id_user")
                .HasMaxLength(255);

            builder
                .Property(x => x.Title)
                .HasColumnName("title")
                .HasMaxLength(255);

            builder
                .Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

            builder
                .Property(x => x.DueDate)
                .HasColumnName("due_date");

            builder
                .Property(x => x.FinishDate)
                .HasColumnName("finish_date");

            builder
                .Property(x => x.Status)
                .HasColumnName("status")
                .HasMaxLength(255);

            builder
                .Property(x => x.Priority)
                .HasColumnName("priority")
                .HasMaxLength(255);

        }
    }
}
