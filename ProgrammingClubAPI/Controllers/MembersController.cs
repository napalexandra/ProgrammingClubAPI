using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClubAPI.CQRS.Queries;
using ProgrammingClubAPI.Helpers;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Models.CreateOrUpdateModels;
using ProgrammingClubAPI.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgrammingClubAPI.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;
        private readonly IMediator _mediator;
        public MembersController(IMembersService membersService, IMediator mediator)
        {
            _membersService = membersService;
            _mediator = mediator;
        }
        #region Implement Versioning By Method

        // GET: api/<MembersController>
        [HttpGet]
        [MapToApiVersion("1.0")]
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

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedMembers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = new GetAllMembersPagedQuery(pageNumber, pageSize);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetV2()
        {
            try
            {
                var members = await _membersService.GetAllMembersAsync();
                if (members.Count() <= 0)
                {
                    //return NotFound("No members found.");
                    return StatusCode((int)HttpStatusCode.OK, ErrorMessagesEnum.NoMembersFound);
                }
                return Ok("Return V2");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion








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
        // [Authorize(Roles = "Admin,Member")]
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
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _membersService.DeleteMemberAsync(id);
                if (result == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.MemberNotFound);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.MemberRemoved);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
