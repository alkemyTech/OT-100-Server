using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Comments;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _service;

        public CommentsController(CommentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Comments", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success", typeof(GetCommentsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult<List<GetCommentsDto>>> GetAll()
        {
            return await _service.GetComments();
        }

        [HttpPut("public{id}")]
        [Authorize(Roles = "Admin,User")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Comment", Description = "Requires admin/user privileges")]
        [SwaggerResponse(204, "Updated. Returns nothing.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCommentDto model)
        {
            await _service.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft Delete an existing Comment", Description = "Requires admin/user privileges")]
        [SwaggerResponse(204, "Deleted. Returns nothing.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
        
        [HttpPost]
        [Authorize]
        #region Documentation
        [SwaggerOperation(Summary = "Create Comment.",Description = "Requires user privileges.")]
        [SwaggerResponse(200, "Created. Returns the id of the created object.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<int>> Create([FromBody] CreateCommentDto commentDto)
        {
            return await _service.CreateComment(commentDto);
        }
    }
}