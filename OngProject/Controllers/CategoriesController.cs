using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OngProject.Application.DTOs.Categories;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [SwaggerTag("Create, Read, Update and Delete Categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Categories", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns a list of existing Categories.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user or wrong jwt token.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get slide details by id", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns the category details.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Create Category", Description = ".")]
        [SwaggerResponse(200, "Created. Returns the id of the created object.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Post([FromBody] CreateCategoryDto model)
        {
            return Ok(await _service.CreateCategory(model));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Category", Description = "Requires admin privileges")]
        [SwaggerResponse(204, "Updated. Returns nothing.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Put(int id, [FromBody] CreateCategoryDto model)
        {
            await _service.Update(id,model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft delete an existing Category", Description = "Requires admin privileges")]
        [SwaggerResponse(204, "Deleted. Returns nothing.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}