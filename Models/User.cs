using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arna_Project_Track.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string? Password { get; set; } = "password@123";

        public string? Role { get; set; } = "Employee";

        [Display(Name = "Employee Role")]
        public int? EmployeeRole { get; set; }

        public string? Department { get; set; }

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTime? JoiningDate { get; set; }

        [ForeignKey("EmployeeRole")]
        public EmployeeRole? EmployeeRoleNavigation { get; set; }

        public virtual ActiveUser? ActiveUser { get; set; }
    }
}
