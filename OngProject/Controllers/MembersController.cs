using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Members;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/members")]
    [SwaggerTag("Create, Read, Update and Delete Members")]
    public class MembersController : ControllerBase
    {
        private readonly MemberService _service;

        public MembersController(MemberService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Members.", Description = "Requires admin privileges.")]
        [SwaggerResponse(200, "Success. Returns a list of existing Members.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<List<GetMembersDto>>> GetAll()
        {
            return await _service.GetMembers();
        }

        [HttpGet("{id}")]
        #region Documentation
        [SwaggerOperation(Summary = "Get member details by id.", Description = ".")]
        [SwaggerResponse(200, "Success. Returns the slide details.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        //[SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetMembersDto>> GetById(int id)
        {
            return await _service.GetMemberDetails(id);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Create Member.",Description = "Requires user or admin privileges.")]
        [SwaggerResponse(200, "Created. Returns the id of the created object.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<int>> Create(CreateMemberDto memberDto)
        {
            return await _service.CreateMember(memberDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User, Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Member.", Description = ".")]
        [SwaggerResponse(204, "Updated. Returns nothing.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        //[SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Update(int id, CreateMemberDto memberDto)
        {
            await _service.UpdateMember(id, memberDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft delete an existing Member.", Description = "Requires admin privileges.")]
        [SwaggerResponse(204, "Deleted. Returns nothing.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteMember(id);

            return NoContent();
        }
    }
}