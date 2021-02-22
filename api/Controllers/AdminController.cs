using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> UsersWithRoles()
        {
            var users = await _userManager.Users
                .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select( u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }


        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public ActionResult GetPhotosForModeration()
        {
            return Ok("Only Admins or Moderators can see this");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var newRoles = roles.Split(",").ToArray();     // comma separated values into array

            var user = await _userManager.FindByNameAsync(username);    // get user model for suername

            if (user == null) return NotFound("Could not find user");   

            var userExistingRoles = await _userManager.GetRolesAsync(user); 

            var result = await _userManager.AddToRolesAsync(user, newRoles.Except(userExistingRoles));  
                    //add new roles- the ones missing from userExisting Roles but present in newRoles

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userExistingRoles.Except(newRoles));
                    //this delete roles that exist but not present in newRoles

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

    }
}