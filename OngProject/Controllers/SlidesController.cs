using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Slides;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/slides")]
    public class SlidesController : ControllerBase
    {
        private readonly SlideService _service;

        public SlidesController(SlideService service)
        {
            _service = service;
        }

        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get slide details by id", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns the slide details.", typeof(GetSlideDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        #endregion
        public async Task<ActionResult<GetSlideDetailsDto>> GetById(int id)
        {
            return await _service.GetSlideDetailsDto(id);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Slide", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Updated. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        #endregion
        public async Task<ActionResult> Update(int id, UpdateSlideDto slideDto)
        {
            await _service.UpdateSlide(id, slideDto);

            return NoContent();
        }
    }
        
}