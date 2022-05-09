using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models.DTOs
{
    public class DisciplineDTO
    {
        public string Name { get; set; }
        public bool IsOptional { get; set; }
        public int? FacultyId { get; set; }
        public int NumberOfStudents { get; set; }
        public int MaxNumberOfStudents { get; set; }
        public int Year { get; set; }
    }
}
