using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Activities;
using OngProject.Application.Services;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityService _service;

        public ActivitiesController(ActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetActivitiesDto>>> GetAll()
        {
            return await _service.GetActivities();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetActivityDetailsDto>> GetById(int id)
        {
            return await _service.GetActivityDetails(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateActivityDto activityDto)
        {
            return await _service.CreateActivity(activityDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateActivityDto activityDto)
        {
            await _service.UpdateActivity(id, activityDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteActivity(id);

            return NoContent();
        }
    }
}