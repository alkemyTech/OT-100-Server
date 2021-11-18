using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.News;
using OngProject.Application.Helpers.Wrappers;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/news")]
    [SwaggerTag("Create, Read, Update and Delete News")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _service;

        public NewsController(NewsService service)
        {
            _service = service;
        }

        // ==================== Get All ==================== //
        [HttpGet]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "List of all News", Description = ".")]
        [SwaggerResponse(200, "Success. Returns a list of existing News.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<Pagination<GetNewsDto>>> GetAll([FromQuery] NewsQueryDto queryDto)
        {
            return await _service.GetNews(queryDto);
        }

        // ==================== Get By Id ==================== //
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get News details by id", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns the News details")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetNewsDetailsDto>> GetById(int id)
        {
            return await _service.GetNewsDetails(id);
        }

        // ==================== Post News ==================== //
        [HttpPost]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Create News", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Created. Returns the id of the created object.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<int>> Create([FromForm] CreateNewsDto newsDto)
        {
            return await _service.CreateNews(newsDto);
        }

        // ==================== Update News ==================== //
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing News", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Updated. Returns the object News updated")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetNewsDetailsDto>> Update(int id, [FromForm] CreateNewsDto newsDto)
        {
            await _service.UpdateNews(id, newsDto);

            return await _service.GetNewsDetails(id);
        }

        // ==================== Soft Delete News ==================== //
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft delete an existing News", Description = "Requires admin privileges")]
        [SwaggerResponse(204, "Deleted. Returns nothing.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteNews(id);

            return NoContent();
        }
    }
}