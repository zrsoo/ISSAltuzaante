using AcademicInfo.Models;
using AcademicInfo.Models.DTOs;
using AcademicInfo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly DisciplineService _disciplineService;

        public DisciplineController(DisciplineService disciplineService)
        {
            this._disciplineService = disciplineService;
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
    }
}
