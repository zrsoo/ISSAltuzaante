using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;

namespace AcademicInfo.Services
{
    public interface IDisciplineService
    {
        public Task<List<Discipline>> GetAll();

        public Task<int> AddDiscipline(DisciplineDTO disciplineDTO);
        
    }
}
