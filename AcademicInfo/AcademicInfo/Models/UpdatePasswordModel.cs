using System.ComponentModel.DataAnnotations;

namespace AcademicInfo.Models
{
    public class UpdatePasswordModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
    }
}
