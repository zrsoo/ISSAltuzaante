using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicInfo.Models.DTOs
{
    public class PreferenceDTO
    {
        public int? OptionalId { get; set; }
        public int? Preference { get; set; }
      
    }
}
