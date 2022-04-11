using AcademicInfo.Models;
using System.ComponentModel.DataAnnotations;

namespace InternshipBackend.Data
{
    public class RegisterModel
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

        //TODO: include all fields and add in corresponding table (student/teacher) according to IsTeacher
        public string? City { get; set; }
        public int? Year { get; set; }
        //public string Degree { get; set; }
        //public bool IsTeacher { get; set; }
    }
}
