using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.News;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/news")]
    [SwaggerTag("Create, read, update and delete News")]
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
        [SwaggerOperation(Summary = "List of all News",Description = "Unnecessary admin privileges")]
        [SwaggerResponse(200, "Success. Returns a list of existing News", typeof(GetNewsDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        #endregion

        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetNews());
        }

        // ==================== Get By Id ==================== //
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get News details by id",Description = "Unnecessary admin privileges")]
        [SwaggerResponse(200, "Success. Returns the News details", typeof(GetNewsDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        #endregion
        public async Task<ActionResult<GetNewsDetailsDto>> GetById([SwaggerParameter("ID of an existing News")]int id)
        {
            return await _service.GetNewsDetails(id);
        }

        // ==================== Post News ==================== //
        [HttpPost]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Create News",Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Created. Returns the object News created", typeof(CreateNewsDto))]
        [SwaggerResponse(400, "BadRequest. Object not created, try again")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult<int>> Create([SwaggerParameter("Object parameters")]CreateNewsDto newsDto)
        {
            return await _service.CreateNews(newsDto);
        }

        // ==================== Update News ==================== //
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing News",Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Updated. Returns the object News updated", typeof(GetNewsDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Object not updated, try again")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult<GetNewsDetailsDto>> Update([SwaggerParameter("ID of an existing News")]int id, CreateNewsDto newsDto)
        {
            await _service.UpdateNews(id, newsDto);

            return await _service.GetNewsDetails(id);
        }

        // ==================== Soft Delete News ==================== //
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft delete an existing News",Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Deleted. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Object not deleted, try again")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Delete([SwaggerParameter("ID of an existing News")]int id)
        {
            await _service.SoftDeleteNews(id);

            return NoContent();
        }
    }
}