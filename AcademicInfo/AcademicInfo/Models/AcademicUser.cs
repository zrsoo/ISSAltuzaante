using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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
        
    }
}
