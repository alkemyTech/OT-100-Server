using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Testimonials;
using OngProject.Application.Services;

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
        public async Task<ActionResult> Update(int id, CreateTestimonyDto testimonyDto)
        {
            await _service.UpdateTestimony(id, testimonyDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteTestimony(id);

            return NoContent();
        }
    }
}