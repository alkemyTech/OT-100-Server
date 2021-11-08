using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Members;
using OngProject.Application.Services;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase
    {
        private readonly MemberService _service;

        public MembersController(MemberService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<GetMembersDto>>> GetAll()
        {
            return await _service.GetMembers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMemberDetailsDto>> GetById(int id)
        {
            return await _service.GetMemberDetails(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMemberDto memberDto)
        {
            return await _service.CreateMember(memberDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateMemberDto memberDto)
        {
            await _service.UpdateMember(id, memberDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteMember(id);

            return NoContent();
        }
    }
}