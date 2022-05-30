using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models
{
    public class Teacher : AcademicUser
    {
        [ForeignKey("AspNetUser")]
        public int Id { get; set; }

        public string Degree { get; set; }
    }
}