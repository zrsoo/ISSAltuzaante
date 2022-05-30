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
        private readonly GradeService _gradeService;
        private readonly UserManager<AcademicUser> _userManager;
        private readonly IUserService _userService;

        public DisciplineController(DisciplineService disciplineService, GradeService gradeService, UserManager<AcademicUser> userManager, IUserService userService)
        {
            _disciplineService = disciplineService;
            _gradeService = gradeService;
            _userManager = userManager;
            _userService = userService;
        }
        
        //add a new discipline if the request is successful
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

        //retrieve all the optionals for the teacher with the role of chief of department
        [HttpGet]
        [Route("get-all-optionals")]
        [Authorize(Roles = "Teacher")]
        public async Task<List<Discipline>> getAllOptionals()
        {
            //using the token, we check if the logged in user is teacher, then if it is chiefOfDepartment
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

        //update a discipline if the request is successful
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

        //get all the discipline for a student of a certain year if there is a logged student
        [HttpGet]
        [Route("view-curriculum")]
        [Authorize(Roles = "Student")]
        public async Task<List<Discipline>> GetDisciplinesByYear()
        {
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

        //view all the optional courses for the current student of a certain year if there is a logged one
        [HttpGet]
        [Route("view-optionals")]
        [Authorize(Roles = "Student")]
        public async Task<List<Discipline>> viewOptionals()
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

            int year = int.Parse(user.Year);

            List<Discipline> disciplines = await _disciplineService.GetAll();
            return disciplines.FindAll(d => d.IsOptional == true && d.Year == year);
        }

        //assign optionals to students by their preferences in descending order (beginning with the most wanted)
        [HttpPatch]
        [Route("assign-optional")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> AssignOptional([FromBody] List<PreferenceDTO> preferences)
        {
            var l = preferences.OrderByDescending(e => e.Preference).ToList();

            int? optional = -1;
            List<Discipline> disciplines = await _disciplineService.GetAll();
            for (int i = 0; i < l.Count(); i++)
            {
                if (disciplines.FindLast(d => d.MaxNumberOfStudents > d.NumberOfStudents && d.DisciplineId == l[i].OptionalId) != null)
                {
                    optional = l[i].OptionalId;
                    break;
                }
            }
            Console.WriteLine(optional);
            if (optional != -1)
            {
                String email = User.FindFirst("Email")?.Value;
                if (email == null)
                    return null;
                await _userService.UpdateDisciplineAsync(email, (int)optional);

                await _disciplineService.IncreaseOptionalDiscipline((int)optional);

                return Ok(new Response { Success = true, Message = "Assigned discipline" + optional + " successfully!" });
            }
            else
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "All discipline are not available"
                });
        }

        //get the disciplines ordered descending by their average grade
        [HttpGet]
        [Route("get-disciplines-ranked-grade-avg")]
        [Authorize(Roles = "Teacher")]
        public async Task<List<Discipline>> GetDisciplinesRankedByAvgGrade()
        {
            return await this._disciplineService.GetRankedByAvgGrade();
        }

        //get the disciplines for the current logged teacher if there is one
        [HttpGet]
        [Route("teacher")]
        [Authorize(Roles = "Teacher")]
        public async Task<List<Discipline>> getDisciplinesByTeacher()
        {
            //using the token, we find current teacher's email bla bla new merge
            //            //test recover changes
            //test recover changes
            //test recover changes
            String email = User.FindFirst("Email")?.Value;
            if (email == null)
                return null;

            List<Discipline> disciplines = await _disciplineService.GetAll();
            return disciplines.FindAll(d => d.TeacherEmail == email);
        }

        //get the count of optionals done by the current logged teacher if there is one
        [HttpGet]
        [Route("number-optionals")]
        [Authorize(Roles = "Teacher")]
        public async Task<int> GetOptionalsCount()
        {
            String email = User.FindFirst("Email").Value;
            if (email == null)
                return -1;

            return (await _disciplineService.GetAll()).FindAll(discipline => discipline.IsOptional == true 
            && discipline.TeacherEmail == email).Count();
        }

        //the curent logged teacher can access a list with all the students enrolled to a certain discipline
        [HttpGet]
        [Route("{disciplineId}/students")]
        [Authorize(Roles = "Teacher")]
        public async Task<List<UserDTO>> getStudentsForCurrentDiscipline(int disciplineId)
        {
            //using the token, we find current teacher's email
            //String email = User.FindFirst("Email")?.Value;
            //if (email == null)
            //    return null;
            //test recover changes

            Discipline discipline = await _disciplineService.GetById(disciplineId);
            List<AcademicUser> users = await _userManager.Users.ToListAsync();
            List<UserDTO> result = new List<UserDTO>();
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (user.DisciplineId == discipline.DisciplineId)
                        result.Add(new UserDTO(user));

                    if (user.Year == discipline.Year.ToString() && user.FacultyId == discipline.FacultyId)
                        result.Add(new UserDTO(user));
                }
            }

            return result;
        }

        //get the disciplines done by a teacher only in an universitary year
        [HttpGet]
        [Route("get-disciplines-by-teacher-year")]
        public async Task<List<Discipline>> GetDisciplinesByTeacherYear([FromQuery(Name = "email")] string email, 
            [FromQuery(Name = "year")] int year)
        {
            return await _disciplineService.getDisciplinesByTeacherYear(email, year);
        }
    }
}