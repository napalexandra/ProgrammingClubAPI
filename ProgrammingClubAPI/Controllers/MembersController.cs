using Microsoft.AspNetCore.Mvc;
using ProgrammingClubAPI.Services;

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
        public async Task<IActionResult> Get()
        {
            try { 
                var members = await _membersService.GetAllMembersAsync();
                if (members == null || !members.Any())
                {
                    return NotFound("No members found.");
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MembersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
