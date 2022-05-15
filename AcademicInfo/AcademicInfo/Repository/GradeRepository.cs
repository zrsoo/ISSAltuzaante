using AcademicInfo.Config;
using AcademicInfo.Models;
using AcademicInfo.Services;
using Microsoft.EntityFrameworkCore;

namespace AcademicInfo.Repository
{
    public class GradeRepository
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly ICurrentUserService _currentUserService;

        public GradeRepository(ApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService)
        {
            _dataContext = applicationDbContext;
            _currentUserService = currentUserService;
        }
        //needs to be called after every modification on tables
        public int SaveChanges()
        {
            var result = _dataContext.SaveChanges();
            return result;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var results = await _dataContext.SaveChangesAsync(cancellationToken);
            return results;
        }

        public IQueryable<Grade> Get()
        {
            return _dataContext.Set<Grade>();
        }

        public async Task<List<Grade>> GetAll()
        {
            return await _dataContext.Set<Grade>().ToListAsync();
        }

        public Grade Insert(Grade Grade)
        {
            return _dataContext.Set<Grade>().Add(Grade).Entity;
        }

        public Grade Update(Grade grade)
        {
            return _dataContext.Set<Grade>().Update(grade).Entity;
        }

        public async Task<Grade> GetByIdAsync(int id)
        {
            return await _dataContext.Set<Grade>().FirstOrDefaultAsync(i => i.GradeId == id);
        }

        //public async Task<List<Grade>> GetByYear(int year)
        //{
        //    return await _dataContext.Set<Grade>().Where(i => i.Year == year).ToListAsync();
        //}
    }
}
