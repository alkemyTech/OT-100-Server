using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Newss;
using OngProject.Application.Services;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _service;

        public NewsController(NewsService service)
        {
            _service = service;
        }

        // ==================== Get All ==================== //
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetNews());
        }

        // ==================== Get By Id ==================== //
        [HttpGet("{id}")]
        public async Task<ActionResult<GetNewsDetailsDto>> GetById(int id)
        {
            return await _service.GetNewsDetails(id);
        }

        // ==================== Post News ==================== //
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateNewsDto newsDto)
        {
            return await _service.CreateNews(newsDto);
        }

        // ==================== Update News ==================== //
        [HttpPut("{id}")]
        public async Task<ActionResult<GetNewsDetailsDto>> Update(int id, CreateNewsDto newsDto)
        {
            await _service.UpdateNews(id, newsDto);

            return await _service.GetNewsDetails(id);
        }

        // ==================== Soft Delete News ==================== //
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteNews(id);

            return NoContent();
        }
    }
}