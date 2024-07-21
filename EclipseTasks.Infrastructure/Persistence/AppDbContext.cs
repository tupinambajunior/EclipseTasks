using EclipseTasks.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EclipseTasks.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
            .UseSqlServer("Server=db,1433;Database=eclipseworks;User ID=sa;Password=Eclipseworks_user_sa;Trusted_Connection=False; TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            Seeds(modelBuilder);
        }

        private void Seeds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = Guid.Parse("CBF07752-A3FA-4EAD-9F3C-5E597855A455"),
                    Email = "gustavo@eclipseworks.com.br",
                    AccessMode = Core.Enums.AccessMode.Common
                }, new User
                {
                    Id = Guid.Parse("1137DCD1-64A8-4349-B47F-EE54D6A37006"),
                    Email = "gestor@eclipseworks.com.br",
                    AccessMode = Core.Enums.AccessMode.Administrator
                });
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Core.Entities.Task> Tasks { get; set; }

        public DbSet<HistoryTask> Histories { get; set; }

        public DbSet<CommentTask> Comments { get; set; }

        public DbSet<User> Users { get; set; }
    }
}