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
        //TODO: Add role field to AcademicUser
        //TODO: refactor the DB, AspNetUsers holds AcademicUsers fields. Add 2 more tables Teacher and Student with corresponding fields
        //that hold a foreign key to the corresponding element in AspNetUsers
        //public string Role { get; set; } = null;
        public string? City { get; set; }
        public string? Year { get; set; }
    }
}
