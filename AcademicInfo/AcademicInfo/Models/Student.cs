using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models
{
    public class Student : User
    {
        [Key]
        private string StudentId { get; set; }
        private string City { get; set; } = string.Empty;
        private int Year { get; set; }  
        [ForeignKey ("SpecializationId")]
        private int SpecializationId { get; set; }
    }
}
