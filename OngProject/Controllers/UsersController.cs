using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OngProject.Application.DTOs.Users;
using OngProject.Application.Services;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetUsers());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDetailsDto>> GetById(int id)
        {
            return await _service.GetUserDetails(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateUserDto userDto)
        {
            return await _service.CreateUser(userDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateUserDto userDto)
        {
            await _service.UpdateUser(id, userDto);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteUsers(id);

            return NoContent();
        }
    }
}
