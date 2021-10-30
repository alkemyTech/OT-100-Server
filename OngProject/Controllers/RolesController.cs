using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Roles;
using OngProject.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _service;

        public RolesController(RoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRolesDto>>> GetAll()
        {
            return Ok(await _service.GetRoles());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetRolesDto>> GetById(int id)
        {
            return await _service.GetRoleById(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateRoleDto roleyDto)
        {
            return await _service.CreateRole(roleyDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateRoleDto roleyDto)
        {
            await _service.UpdateRole(id, roleyDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteRole(id);

            return NoContent();
        }


    }
}
