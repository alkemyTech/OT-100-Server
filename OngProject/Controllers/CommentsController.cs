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
        public async Task<IEnumerable<GetCommentsDto>> GetAll()
        {
            return await _service.GetComments();
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