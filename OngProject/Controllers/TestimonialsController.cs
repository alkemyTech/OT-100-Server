using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Testimonials;
using OngProject.Application.Helpers.Wrappers;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/testimonials")]
    [SwaggerTag("Create, Read, Update and Delete Testimonials")]
    public class TestimonialsController : ControllerBase
    {
        private readonly TestimonyService _service;

        public TestimonialsController(TestimonyService service)
        {
            _service = service;
        }

        [HttpGet]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Testimonials.", Description = ".")]
        [SwaggerResponse(200, "Success. Returns a list of existing Slides.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<Pagination<GetTestimonialsDto>>> GetAll([FromQuery] TestimonialsQueryDto queryDto)
        {
            return await _service.GetTestimonials(queryDto);
        }

        [HttpGet("{id}")]
        #region Documentation
        [SwaggerOperation(Summary = "Get testimony details by id.", Description = ".")]
        [SwaggerResponse(200, "Success. Returns the slide details.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetTestimonialsDto>> GetById(int id)
        {
            return await _service.GetTestimonyById(id);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Create a Testimony.",Description = "Requires admin privileges.")]
        [SwaggerResponse(200, "Created. Returns the id of the created object.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<int>> Create(CreateTestimonyDto activityDto)
        {
            return await _service.CreateTestimony(activityDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Testimony",Description = "Requires admin privileges")]
        [SwaggerResponse(204, "Updated. Returns nothing.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetTestimonialsDto>> Update(int id, CreateTestimonyDto testimonyDto)
        {
            await _service.UpdateTestimony(id, testimonyDto);
            return await GetById(id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft delete an existing Testimony", Description = "Requires admin privileges")]
        [SwaggerResponse(204, "Deleted. Returns nothing.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteTestimony(id);

            return NoContent();
        }
    }
}