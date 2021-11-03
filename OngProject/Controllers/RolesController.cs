using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Roles;
using OngProject.Application.Services;
using OngProject.Domain.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _service;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleService service, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _roleManager = roleManager;
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
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleyDto.Name);

            if (roleExist) 
                return BadRequest("The Role already exist.");
            
            var newRole = new IdentityRole()
            {
                Name = roleyDto.Name
            };
                
            var roleResult = await _roleManager.CreateAsync(newRole);

            // We need to check if the role has been added successfully
            if(roleResult.Succeeded)
            {
                var role = new Role
                {
                    IdentityId = new Guid(newRole.Id),
                    Name = roleyDto.Name,
                    Description = roleyDto.Description,
                };
                
                var roleId = await _service.CreateRole(role);
                
                return Ok($"The role {roleyDto.Name} has been added successfully with id {roleId}");
            } else 
                return BadRequest("Error when registering Role.");

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