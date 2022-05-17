using AcademicInfo.Config;
using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AcademicUser> _userManager;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _dbContext;

        public UserController(UserManager<AcademicUser> userManager, IUserService userService,
            ICurrentUserService currentUserService, ApplicationDbContext dbContext)
        {
            _userService = userService;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _dbContext = dbContext;
        }

        [HttpPatch]
        [Route("updateuser")]
        [Authorize]

        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserModel user)
        {

            var currentUserEmail = _currentUserService.GetUserId();
            user.Email = currentUserEmail;

            if (currentUserEmail != null)
            {
                try
                {
                    await _userService.UpdateAsync(user);
                    return Ok(new Response { Success = true, Message = "User updated successfully!" });
                }
                catch (ArgumentException exc)
                {
                    return BadRequest(new Response
                    {
                        Success = false,
                        Message = "The user could not be updated due to bad arguments",
                        Errors = new List<String> { exc.Message }
                    });
                }
                catch (Exception exc)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Success = false,
                        Message = "The user could not be updated",
                        Errors = new List<String> { exc.Message }
                    });
                }
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Success = false, Message = "Error updating user!" });
        }

        [HttpPatch]
        [Route("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordModel user)
        {
            var currentUserEmail = _currentUserService.GetUserId();
            user.Email = currentUserEmail;

            if (currentUserEmail != null)
            {
                if (user.NewPassword == user.Password)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response
                            { Success = false, Message = "The new password has to be different from the old one!" });
                }

                if (user.NewPassword != user.NewPasswordConfirm)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response
                        {
                            Success = false, Message = "The password confirmation does not match the new password!"
                        });
                }

                var result = await _userService.UpdatePasswordAsync(user);
                if (result.Success == true)
                {
                    return Ok(new Response { Success = true, Message = "User password updated successfully!" });
                }
                else return StatusCode(StatusCodes.Status500InternalServerError, result);

            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Success = false, Message = "Error updating user!",
                        Errors = new List<string> { "Email doesn't exist!" }
                    });

            ;
        }

        [HttpGet]
        [Route("get-authenticated-user")]
        public async Task<ActionResult> getAuthenticatedUserByTokenAsync()
        {
            String email = User.FindFirst("Email")?.Value;
            if (email == null)
                return null;

            AcademicUser user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return null;
            UserDTO userDTO = new UserDTO(user);

            return StatusCode(200, _currentUserService.GetUserId());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("group-statistics/{specializationId}")]
        public async Task<List<Grade>> GetStatistics(int specializationId)
        {
            List<AcademicUser> students_unfiltered = await _userService.GetAllStudents();
            var students = students_unfiltered.FindAll(s => s.SpecializationId == specializationId);
            List<Grade> grades = await _userService.GetAllGrades();
            var ordered_grades = grades.OrderByDescending(g => g.Mark).ToList();
            var kept_grades = ordered_grades.FindAll(g => students.FindIndex(o => o.Email == g.StudentEmail) >= 0);

            return kept_grades;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("year-statistics/{year}")]
        public async Task<List<GradeDTO>> GetStatisticsYear(int year)
        {
            List<AcademicUser> students_unfiltered = await _userService.GetAllStudents();
            var students = students_unfiltered.FindAll(s => s.Year == year.ToString());
            List<Grade> grades = await _userService.GetAllGrades();
            // group by statement
            var avg_grades = grades.GroupBy(t => new { ID = t.StudentEmail })
                .Select(g => new GradeDTO
                {
                    Average = g.Average(p => p.Mark),
                    ID = g.Key.ID
                });
            var ordered_grades = avg_grades.OrderByDescending(g => g.Average).ToList();
            var kept_grades = ordered_grades.FindAll(g => students.FindIndex(o => o.Email == g.ID) >= 0);

            return kept_grades;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("grant-scholarships/{year}")]
        public async Task<List<AcademicUser>> GrantScholarships(int year)
        {
            List<AcademicUser> students_unfiltered = await _userService.GetAllStudents();
            var students = students_unfiltered.FindAll(s => s.Year == year.ToString());
            List<Grade> grades = await _userService.GetAllGrades();
            // group by statement (top 10 students per year)
            var avg_grades = grades.GroupBy(t => new { ID = t.StudentEmail })
                .Select(g => new GradeDTO
                {
                    Average = g.Average(p => p.Mark),
                    ID = g.Key.ID
                });
            var ordered_grades = avg_grades.OrderByDescending(g => g.Average).ToList().Take(10).ToList(); //top 10 students
            var kept_grades = ordered_grades.FindAll(g => students.FindIndex(o => o.Email == g.ID) >= 0);
            await _userService.grantScholarships(kept_grades);

            return students.FindAll(s=>s.PhoneNumber == "scholarship");
        }
    }


}
