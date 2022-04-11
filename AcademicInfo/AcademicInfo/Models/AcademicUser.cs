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
        //fields to be removed, added to Student
        public string? City { get; set; }
        public string? Year { get; set; }
    }
}
