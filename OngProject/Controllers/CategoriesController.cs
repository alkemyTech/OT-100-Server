using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OngProject.Application.DTOs.Categories;
using OngProject.Application.Services;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCategoryDto model)
        {
            return Ok(await _service.CreateCategory(model));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateCategoryDto model)
        {
            await _service.Update(id,model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}