using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models;

public class TeacherRegisterModel
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

    [Required(ErrorMessage = "Degree is required")]
    public string? Degree { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public bool? IsChiefOfDepartment { get; set; }

    [Required(ErrorMessage = "Faculty is required")]
    [ForeignKey("Faculty")]
    public int? FacultyId { get; set; }
}