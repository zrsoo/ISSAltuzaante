using System.ComponentModel.DataAnnotations;

namespace AcademicInfo.Models
{
    public class UpdateUserModel
    {

        [Key]
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
