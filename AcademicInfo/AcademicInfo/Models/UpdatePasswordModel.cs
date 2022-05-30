using System.ComponentModel.DataAnnotations;

namespace AcademicInfo.Models
{
    public class UpdatePasswordModel
    {
        [Key]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }

        [Required(ErrorMessage = "A New Password is required")]
        public string? NewPassword { get; set; }

        public string? NewPasswordConfirm { get; set; }
    }
}