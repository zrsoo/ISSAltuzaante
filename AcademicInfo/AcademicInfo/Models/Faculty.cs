using System.ComponentModel.DataAnnotations;

namespace AcademicInfo.Models;

public class Faculty
{
    [Key]
    public int FacultyId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}