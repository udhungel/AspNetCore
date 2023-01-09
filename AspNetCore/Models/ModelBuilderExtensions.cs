using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EmployeeManagement_AspNetCore.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Employee>().HasData(
                    new Employee() {
                                    Id = 1,
                                    Department = Dept.IT,
                                    Email = "mark@gmail.com",
                                    Name = "Mark"
                               },
                    new Employee() {
                                    Id = 2,
                                    Department = Dept.HR,
                                    Email = "john@gmail.com",
                                    Name = "John"
                                    }
                                                 );
            
        }
    }
}
