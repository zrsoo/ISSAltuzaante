using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models;

public class Specialization
{
    [Key]
    public int SpecializationId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Degree is required")]
    public string Degree { get; set; }

    [Required(ErrorMessage = "Faculty is required")]
    [ForeignKey("Faculty")]
    public int? FacultyId { get; set; }
}