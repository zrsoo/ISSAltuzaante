using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models
{
    public class Student : User
    {
        private string City { get; set; } = string.Empty;
        private int Year { get; set; }  
        [ForeignKey ("SpecializationId")]
        private int SpecializationId { get; set; }
    }
}
