using DottoressaIorio.BlazorApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.BlazorApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
         
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property(m => m.ConcurrencyStamp).HasMaxLength(50).IsRequired();
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(m => m.ConcurrencyStamp).HasMaxLength(50).IsRequired();
            });
        }
    }
}
