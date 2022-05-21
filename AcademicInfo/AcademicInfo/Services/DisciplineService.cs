using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Repository;

namespace AcademicInfo.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly DisciplineRepository _disciplineRepository;
        private readonly GradeRepository _gradeRepository;

        public DisciplineService(DisciplineRepository disciplineRepository, GradeRepository gradeRepository)
        {
            _disciplineRepository = disciplineRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<List<Discipline>> GetAll()
        {
            return await _disciplineRepository.GetAll();
        }

        public IQueryable<Discipline> Get()
        {
            return _disciplineRepository.Get();
        }

        public async Task<int> AddDiscipline(DisciplineDTO disciplineDTO)
        {
            if (disciplineDTO != null)
            {
                var discipline = new Discipline
                {
                    Name = disciplineDTO.Name,
                    IsOptional = disciplineDTO.IsOptional,
                    FacultyId = disciplineDTO.FacultyId,
                    Year = disciplineDTO.Year,
                    TeacherEmail = disciplineDTO.TeacherEmail
                   
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

        public async Task UpdateDiscipline(Discipline discipline, int id)
        {
            if (discipline == null)
            {
                throw new ArgumentNullException("Discipline is null");
            }

            var patchDiscipline = await _disciplineRepository.GetByIdAsync(id);

            if (patchDiscipline != null)
            {
                patchDiscipline.Name = discipline.Name;
                patchDiscipline.FacultyId = discipline?.FacultyId;
                patchDiscipline.NumberOfStudents = discipline?.NumberOfStudents ?? patchDiscipline.NumberOfStudents;
                patchDiscipline.MaxNumberOfStudents = discipline?.MaxNumberOfStudents ?? patchDiscipline.MaxNumberOfStudents;
                patchDiscipline.Year = discipline?.Year ?? patchDiscipline.Year;
                patchDiscipline.IsOptional = discipline?.IsOptional ?? patchDiscipline.IsOptional;
            }
            else
            {
                throw new ArgumentNullException("Discipline is null");
            }

            _disciplineRepository.Update(patchDiscipline);
            await _disciplineRepository.SaveChangesAsync();
        }

        public async Task<List<Discipline>> GetDisciplinesByYear(int year)
        {
            return await _disciplineRepository.GetByYear(year);
        }

        public async Task<Discipline> GetById(int id)
        {
            return await _disciplineRepository.GetByIdAsync(id);
        }

        public async Task IncreaseOptionalDiscipline(int id)
        {
            
            var patchDiscipline = await _disciplineRepository.GetByIdAsync(id);

            if (patchDiscipline != null)
            {
                patchDiscipline.NumberOfStudents = patchDiscipline.NumberOfStudents +  1;
   
            }
            else
            {
                throw new ArgumentNullException("Discipline is null");
            }

            _disciplineRepository.Update(patchDiscipline);
            await _disciplineRepository.SaveChangesAsync();
        }

        private async Task<int> ComputeDisciplineAvgGrade()
        {
            // TODO Treat the case when there are no grades for a discipline.
            var disciplines = new List<Discipline>();

            disciplines = await _disciplineRepository.GetAll();

            float numberOfGrades;
            float sumOfGrades;

            foreach(var discipline in disciplines)
            {
                var grades = await this._gradeRepository.GetByDisciplineId(discipline.DisciplineId);

                numberOfGrades = grades.Count();

                sumOfGrades = grades.
                    Select(grade => grade.Mark).
                    Sum();

                discipline.AverageGrade = sumOfGrades / numberOfGrades;
                _disciplineRepository.Update(discipline);
            }

            return await _disciplineRepository.SaveChangesAsync();
        }

        public async Task<List<Discipline>> GetRankedByAvgGrade()
        {
            var result = await ComputeDisciplineAvgGrade();
            return await _disciplineRepository.GetRankedByAvgGrade();
        }
    }
}
