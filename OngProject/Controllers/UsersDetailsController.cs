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
    [SwaggerTag("Create, Read, Update and Delete Users")]
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
        [SwaggerOperation(Summary = "List of all Users.", Description = "Requires admin privileges.")]
        [SwaggerResponse(200, "Success. Returns a list of existing Users.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _detailsService.GetUsers());
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get user details by id.", Description = "Requires admin privileges.")]
        [SwaggerResponse(200, "Success. Returns the user details.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetUserDetailsDto>> GetById(int id)
        {
            return await _detailsService.GetUserDetails(id);
        }
        
        [Authorize(Roles = "User, Admin")]
        [HttpPatch("{id}")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Slide.", Description = "Requires user privileges.")]
        [SwaggerResponse(204, "Updated. Returns nothing.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
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
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _detailsService.SoftDeleteUsers(id);

            return NoContent();
        }
    }
}
