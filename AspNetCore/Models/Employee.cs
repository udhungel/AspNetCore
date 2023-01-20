using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_AspNetCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Required]        
        public string Email { get; set; }

        //Since its a enum the underlying datatype is value type integar we need to make it  nullable 
        [Required]        
        public Dept? Department { get; set; }

        public string PhotoPath { get; set; } // In the DB we just store the Name 

        public string Position { get; set; }
        
    }
}
