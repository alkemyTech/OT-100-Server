using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Activities;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/activities")]
    [SwaggerTag("Create, Read, Update and Delete Activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityService _service;

        public ActivitiesController(ActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Activities", Description = ".")]
        [SwaggerResponse(200, "Success. Returns a list of existing Activities.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        //[SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<List<GetActivitiesDto>>> GetAll()
        {
            return await _service.GetActivities();
        }

        [HttpGet("{id}")]
        #region Documentation
        [SwaggerOperation(Summary = "Get activity details by id", Description = ".")]
        [SwaggerResponse(200, "Success. Returns the activity details.")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        //[SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<GetActivityDetailsDto>> GetById(int id)
        {
            return await _service.GetActivityDetails(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Create a Activity.", Description = "Requires admin privileges.")]
        [SwaggerResponse(200, "Created. Returns the id of the created object.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(403, "Unauthorized user.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<int>> Create(CreateActivityDto activityDto)
        {
            return await _service.CreateActivity(activityDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Update a Activity",Description = "Require admin privileges")]
        [SwaggerResponse(204, "Success. Returns nothing.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user or wrong jwt token")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Update(int id, CreateActivityDto activityDto)
        {
            await _service.UpdateActivity(id, activityDto);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft delete an existing Activity", Description = ".")]
        [SwaggerResponse(204, "Deleted. Returns nothing.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        //[SwaggerResponse(403, "Unauthorized user")]
        [SwaggerResponse(404, "NotFound. Entity id not found.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteActivity(id);

            return NoContent();
        }
    }
}