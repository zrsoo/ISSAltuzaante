using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models
{
    public class AcademicUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? City { get; set; }
        public string? Year { get; set; }

        public string? Degree { get; set; }

        public bool? IsChiefOfDepartment { get; set; }

        public int? FacultyId { get; set; }

        public int? SpecializationId { get; set; }

        [ForeignKey("Discipline")]
        public int DisciplineId { get; set; } // this is the optional id
    }
}