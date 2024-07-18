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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
               .ToTable("comment");

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
                .Property(x => x.Role)
                .HasColumnName("role")
                .HasMaxLength(255);

            builder
                .Property(x => x.Content)
                .HasColumnName("content")
                .HasMaxLength(255);

            builder
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
