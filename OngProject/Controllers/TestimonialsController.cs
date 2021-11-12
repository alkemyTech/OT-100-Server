using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Testimonials;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/testimonials")]
    public class TestimonialsController : ControllerBase
    {
        private readonly TestimonyService _service;

        public TestimonialsController(TestimonyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTestimonialsDto>>> GetAll()
        {
            return Ok(await _service.GetTestimonials());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTestimonialsDto>> GetById(int id)
        {
            return await _service.GetTestimonyById(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTestimonyDto activityDto)
        {
            return await _service.CreateTestimony(activityDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Testimony",Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Updated. Returns the object Testimony updated", typeof(GetTestimonialsDto))]
        [SwaggerResponse(400, "BadRequest. Object not updated, try again")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult<GetTestimonialsDto>> Update(int id, CreateTestimonyDto testimonyDto)
        {
            await _service.UpdateTestimony(id, testimonyDto);
            return await GetById(id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Delete an existing Testimony", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Succes. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteTestimony(id);

            return NoContent();
        }
    }
}