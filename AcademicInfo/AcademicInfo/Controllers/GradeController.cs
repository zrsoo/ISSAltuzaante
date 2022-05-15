using AcademicInfo.Models;
using AcademicInfo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly GradeService _gradeService;
        private readonly UserManager<AcademicUser> _userManager;

        public GradeController(GradeService gradeService, UserManager<AcademicUser> userManager)
        {
            _gradeService = gradeService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<List<Grade>> getAll()
        {
            return await _gradeService.GetAll();
        }
    }
}
