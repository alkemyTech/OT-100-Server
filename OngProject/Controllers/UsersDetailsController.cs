using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OngProject.Application.DTOs.UsersDetails;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    [SwaggerTag("Create, read, update and delete Users")]
    public class UsersDetailsController : ControllerBase
    {
        private readonly UserDetailsService _detailsService;

        public UsersDetailsController(UserDetailsService detailsService)
        {
            _detailsService = detailsService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "List of all USers", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns a list of existing Organizations", typeof(GetUsersDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _detailsService.GetUsers());
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get a User by ID", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns an existing User", typeof(GetUserDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult<GetUserDetailsDto>> GetById([SwaggerParameter("ID of an existing User")] int id)
        {
            return await _detailsService.GetUserDetails(id);
        }
        
        #region Documentation
        [SwaggerOperation(Summary = "Update personal data of the user.")]
        [SwaggerResponse(204, "Updated.")]
        [SwaggerResponse(400, "Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        #endregion
        [Authorize(Roles = "User, Admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<UpdateUserDetailsDto> patchDocument)
        {
            await _detailsService.PatchUser(id, patchDocument);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft Delete of an existing User", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Delete([SwaggerParameter("ID of an existing User")] int id)
        {
            await _detailsService.SoftDeleteUsers(id);

            return NoContent();
        }
    }
}
