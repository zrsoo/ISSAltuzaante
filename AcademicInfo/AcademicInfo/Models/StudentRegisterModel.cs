using AcademicInfo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipBackend.Data
{
    public class StudentRegisterModel
    {
        [Key]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int? Year { get; set; }
        
        [Required(ErrorMessage = "Specialization is required")]
        [ForeignKey("Specialization")]
        public int? SpecializationId { get; set; }
    }
}
