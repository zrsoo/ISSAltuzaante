using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [Required(ErrorMessage = "Student Email is required")]
        public string StudentEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student Email is required")]
        public string TeacherEmail { get; set; } = string.Empty;

        public int Mark { get; set; }

        [ForeignKey("Discipline")]
        public int DisciplineId { get; set; }
    }
}