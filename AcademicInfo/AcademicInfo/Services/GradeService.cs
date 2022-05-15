using AcademicInfo.Models;
using AcademicInfo.Repository;

namespace AcademicInfo.Services
{
    public class GradeService
    {
        private readonly GradeRepository _gradeRepository;

        public GradeService(GradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<List<Grade>> GetAll()
        {
            return await _gradeRepository.GetAll();
        }

        public IQueryable<Grade> Get()
        {
            return _gradeRepository.Get();
        }

        public async Task<int> AddGrade()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGrade(Grade grade, int id)
        {
            throw new NotImplementedException();
        }

    }
}
