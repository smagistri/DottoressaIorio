using DottoressaIorio.BlazorApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DottoressaIorio.BlazorApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
    }
}
