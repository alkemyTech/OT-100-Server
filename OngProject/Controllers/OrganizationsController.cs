using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OngProject.Application.DTOs.Organizations;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/organizations")]
    [SwaggerTag("Create, read, update and delete Organizations")]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationService _service;

        public OrganizationsController(OrganizationService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Organizations", Description = "Unnecessary admin privileges")]
        [SwaggerResponse(200, "Success. Returns a list of existing Organizations", typeof(GetOrganizationsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        #endregion
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Get details of the organization by ID", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns the Organization data.", typeof(GetOrganizationDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> GetById([SwaggerParameter("ID of an existing News")] int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpGet("public/{id}")]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "Get public details of the organization by ID", Description = "Unnecessary admin privileges")]
        [SwaggerResponse(200, "Success. Returns the public data of the Organization", typeof(GetOrganizationPublicDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        #endregion
        public async Task<ActionResult> GetByIdPublic([SwaggerParameter("ID of an existing Organization")] int id)
        {
            return Ok(await _service.GetByIdPublic(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Creates a new Organization", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Post([FromBody][SwaggerParameter("Object parameters")] CreateOrganizationDto model)
        {
            return Ok(await _service.CreateOrganization(model));
        }

        [HttpPut("public{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Modifies an existing Organization", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Put([SwaggerParameter("ID of an existing Organization")] int id, [FromBody] UpdateOrganizationPublicDto model)
        {
            await _service.Update(id,model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Soft Delete an existing Organization", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Delete([SwaggerParameter("ID of an existing Organization")] int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
