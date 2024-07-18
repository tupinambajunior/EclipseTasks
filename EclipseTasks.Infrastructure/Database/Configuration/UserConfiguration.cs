using EclipseTasks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Infrastructure.Database.Configuration
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("user");

            builder
                .Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100);

            builder
                .Property(x => x.Role)
                .HasColumnName("role")
                .HasMaxLength(255);
        }
    }
}
