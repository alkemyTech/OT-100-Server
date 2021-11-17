using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Contacts;
using OngProject.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service;

        public ContactsController(ContactService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "List of all Contacts",Description = "Require admin privileges")]
        [SwaggerResponse(200, "Success. Returns a list of existing Contacts", typeof(GetContactsDto))]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token")]
        #endregion
        public async Task<ActionResult<IEnumerable<GetContactsDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "Create a Contact.", Description = ".")]
        [SwaggerResponse(200, "Created.")]
        [SwaggerResponse(400, "BadRequest. Object not created, try again.")]
        [SwaggerResponse(401, "Unauthenticated or wrong jwt token.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<int>> Create(CreateContactDto contactDto)
        {
            return await _service.CreateContact(contactDto);
        }

    }
}