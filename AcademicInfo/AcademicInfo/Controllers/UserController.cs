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

        public UserController(UserManager<AcademicUser> userManager, IUserService userService, ICurrentUserService currentUserService, ApplicationDbContext dbContext)
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
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Error updating user!" });
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
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "The new password has to be different from the old one!" });
                }
                else
                {
                    var result = await _userService.UpdatePasswordAsync(user);
                    if (result.Success == true)
                    {
                        return Ok(new Response { Success = true, Message = "User password updated successfully!" });
                    }
                    else return StatusCode(StatusCodes.Status500InternalServerError, result);
                }
            }
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Error updating user!", Errors = new List<string> { "Email doesn't exist!" } }); ;
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
    }
}
