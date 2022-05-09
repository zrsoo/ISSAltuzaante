using AcademicInfo.Config;
using AcademicInfo.Models;
using AcademicInfo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AcademicInfo.Repository
{
    public class DisciplineRepository
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly ICurrentUserService _currentUserService;

        public DisciplineRepository(ApplicationDbContext applicationDbContext,
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

        public IQueryable<Discipline> Get()
        {
            return  _dataContext.Set<Discipline>();
        }

        public async Task<List<Discipline>> GetAll()
        {
            return await _dataContext.Set<Discipline>().ToListAsync();
        }

        public Discipline Insert(Discipline discipline)
        {
            return _dataContext.Set<Discipline>().Add(discipline).Entity;
        }

        public Discipline Update(Discipline discipline)
        {
            return _dataContext.Set<Discipline>().Update(discipline).Entity;
        }

        public async Task<Discipline> GetByIdAsync(int id)
        {
            return await _dataContext.Set<Discipline>().FirstOrDefaultAsync(i => i.DisciplineId == id);
        }

        public async Task<List<Discipline>> GetByYear(int year)
        {
            return await _dataContext.Set<Discipline>().Where(i => i.Year == year).ToListAsync();
        }
    }
}
