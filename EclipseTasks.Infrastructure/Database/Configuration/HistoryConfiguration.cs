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
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder
               .ToTable("hystory");

            builder
                .Property(x => x.IdTask)
                .HasColumnName("id_task")
                .HasMaxLength(100);

            builder
                .Property(x => x.IdUser)
                .HasColumnName("id_user")
                .HasMaxLength(100);

            builder
                .Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

            builder
                .Property(x => x.Changes)
                .HasColumnName("changes")
                .HasMaxLength(1000);

            builder
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
