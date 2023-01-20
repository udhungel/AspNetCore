using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_AspNetCore.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //when you overrise you need to call the base call using base keyword and pass incoming onmodel object
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

    }
}
