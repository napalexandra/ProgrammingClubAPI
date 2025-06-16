using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClubAPI.Helpers;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Models.CreateOrUpdateModels;
using ProgrammingClubAPI.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgrammingClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;
        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        // GET: api/<MembersController>
        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var members = await _membersService.GetAllMembersAsync();
                if (members.Count() <= 0)
                {
                    //return NotFound("No members found.");
                    return StatusCode((int)HttpStatusCode.OK, ErrorMessagesEnum.NoMembersFound);
                }
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Member member = await _membersService.GetMemberByIdAsync(id);
                if (member != null)
                    return StatusCode((int)HttpStatusCode.OK, member);
                return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.MemberNotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<MembersController>
        [HttpPost]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Post([FromBody] Member member)
        {
            try
            {
                if (member != null)
                {
                    await _membersService.AddMemberAsync(member);
                    return StatusCode((int)HttpStatusCode.Created, SuccessMessagesEnum.MemberAdded);
                }
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.InvalidData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.InvalidData);
                }
                var updatedMember = await _membersService.UpdateMemberAsync(id, member);

                if (updatedMember != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.MemberUpdated);
                }

                return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.MemberNotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PatchMember(Guid id, [FromBody] UpdateMemberPartially member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.InvalidData);
                }
                Member updatedMember = await _membersService.UpdateMemberPartiallyAsync(id, member);
                if (updatedMember != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.MemberUpdated);
                }
                return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.MemberNotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _membersService.DeleteMemberAsync(id);
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.MemberRemoved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
