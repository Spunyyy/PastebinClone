using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PastebinClone.Models;

namespace PastebinClone.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Paste>()
            .HasKey(p => p.UrlID);

            modelBuilder.Entity<Paste>()
                .HasIndex(p => p.UrlID)
                .IsUnique();
        }

        public DbSet<Paste> Pastes { get; set; }

    }
}
