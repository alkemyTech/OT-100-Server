using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Organizations;
using OngProject.Application.Services;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _service;

        public OrganizationController(OrganizationService service)
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
        public async Task<ActionResult> Post([FromBody] CreateOrganizationDto model)
        {
            if (ModelState.IsValid)
                return Ok(await _service.CreateOrganization(model));
            else
                return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateOrganizationDto model)
        {
            if (ModelState.IsValid)
            {
                await _service.Update(id,model);
                return Ok();
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
