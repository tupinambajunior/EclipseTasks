using EclipseTasks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Infrastructure
{
    public class PgContext : DbContext
    {
        public PgContext(DbContextOptions<PgContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Core.Entities.ProjectTask> Task { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
