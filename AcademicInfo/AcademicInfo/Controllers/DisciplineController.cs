using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly DisciplineService _disciplineService;
        private readonly UserManager<AcademicUser> _userManager;


        public DisciplineController(DisciplineService disciplineService, UserManager<AcademicUser> userManager)
        {
            _disciplineService = disciplineService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> InsertAsync([FromBody] DisciplineDTO disciplineDTO)
        {
            try
            {
                var id = await _disciplineService.AddDiscipline(disciplineDTO);
                return Ok(new Response { Success = true, Message = "Inserted Discipline " + id + " successfully!" });
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
        [Route("get-all-optionals")]
        [Authorize(Roles = "Teacher")]
        public async Task<List<Discipline>> getAllOptionals()
        {
            //using the token, we check if the logged in user is chiefOfDepartment
            String email = User.FindFirst("Email")?.Value;
            if (email == null)
                return null;

            AcademicUser user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                return null;
            }
            
            if (user.IsChiefOfDepartment == true)
            {
                List<Discipline> disciplines = await _disciplineService.GetAll();
                return disciplines.FindAll(d => d.IsOptional == true);
            }
            else
            {
                return null;
            }
        }
        [HttpPatch]
        [Route("update/{id}")]
        [Authorize(Roles = "Teacher,Student,Admin")]
        public async Task<IActionResult> updateDiscipline([FromBody] Discipline discipline, int id)
        {
            try
            {
                await _disciplineService.UpdateDiscipline(discipline, id);
                return Ok(new Response { Success = true, Message = "Updated discipline successfully!" });
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
        [Route("view-curriculum")]
        [Authorize(Roles = "Student")]
        public async Task<List<Discipline>> GetDisciplinesByYear()
        {
            //using the token, we check if the logged in user is chiefOfDepartment
            String email = User.FindFirst("Email")?.Value;
            if (email == null)
                return null;

            AcademicUser user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                return null;
            }

            if (user.Year != null)
            {
                int year = int.Parse(user.Year);
                List<Discipline> disciplines = await _disciplineService.GetDisciplinesByYear(year);
                return disciplines;
            }
            else
            {
                return null;
            }
        }
    }
}
