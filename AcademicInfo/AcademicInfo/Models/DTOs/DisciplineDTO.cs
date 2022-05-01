using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models.DTOs
{
    public class DisciplineDTO
    {
        public string Name { get; set; }
        public string IsOptional { get; set; }
        public int? FacultyId { get; set; }
    }
}
