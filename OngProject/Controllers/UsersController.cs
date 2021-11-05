using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OngProject.Application.DTOs.Users;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    [SwaggerTag("Create, post, update and delete Users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "List of all USers", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns a list of existing Organizations", typeof(GetUserDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetUsers());
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "Get a User by ID", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns an existing User", typeof(GetUserDetailsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult<GetUserDetailsDto>> GetById([SwaggerParameter("ID of an existing User")] int id)
        {
            return await _service.GetUserDetails(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateUserDto userDto)
        {
            return await _service.CreateUser(userDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([SwaggerParameter("ID of an existing User")] int id, CreateUserDto userDto)
        {
            await _service.UpdateUser(id, userDto);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "Soft Delete of an existing User", Description = "Requires admin privileges")]
        [SwaggerResponse(200, "Success. Returns nothing")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        [SwaggerResponse(403, "Unauthorized user")]
        #endregion
        public async Task<ActionResult> Delete([SwaggerParameter("ID of an existing User")] int id)
        {
            await _service.SoftDeleteUsers(id);

            return NoContent();
        }
    }
}
