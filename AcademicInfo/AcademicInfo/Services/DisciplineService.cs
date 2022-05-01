using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Repository;

namespace AcademicInfo.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly DisciplineRepository _disciplineRepository;

        public DisciplineService(DisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        public async Task<List<Discipline>> GetAll()
        {
            return await _disciplineRepository.GetAll();
        }

        public async Task<int> AddDiscipline(DisciplineDTO disciplineDTO)
        {
            if (disciplineDTO != null)
            {
                var discipline = new Discipline
                {
                    Name = disciplineDTO.Name,
                    IsOptional = disciplineDTO.IsOptional,
                    FacultyId = disciplineDTO.FacultyId
                };
                
                var result = _disciplineRepository.Insert(discipline);
                if (result != null)
                {
                    _disciplineRepository.SaveChanges();
                }

                return result.DisciplineId;
            }
            else
            {
                throw new ArgumentException("Could not save discipline!");
            }

        }
    }
}
