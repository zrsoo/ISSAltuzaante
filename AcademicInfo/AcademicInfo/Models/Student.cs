using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models
{
    public class Student : AcademicUser
    {
        [ForeignKey("AspNetUser")]
        public int Id { get; set; }
        public string? City { get; set; }
        public int? Year { get; set; }
    }
}
