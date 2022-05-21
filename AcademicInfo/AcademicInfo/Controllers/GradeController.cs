using AcademicInfo.Models;
using AcademicInfo.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> InsertAsync([FromBody] Grade grade)
        {
            try
            {
                var id = await _gradeService.AddGrade(grade);

                return Ok(new Response { Success = true, Message = "Inserted grade " + id + " successfully!" });
            }
            catch (ArgumentException exc)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "The discipline could not be added",
                    Errors = new List<String> { exc.Message }
                });
            }
        }

        [HttpGet]
        [Route("student")]
        [Authorize(Roles = "Student")]
        public async Task<List<Grade>> getDisciplinesByTeacher()
        {
            //using the token, we find current teacher's email
            String email = User.FindFirst("Email")?.Value;
            if (email == null)
                return null;

            List<Grade> grades = await _gradeService.GetAll();
            return grades.FindAll(g => g.StudentEmail == email);
        }
    }
}