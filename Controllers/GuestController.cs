using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using TimeZone.Requests;
using TimeZone.Responses;
using TimeZone.Services;

namespace TimeZone.Controllers
{
    [ApiController]
    [Route("guests")]
    public class GuestController : Controller
    {
        private readonly GuestService _guestService;
        private readonly ILogger<GuestController> _logger;

        public GuestController(GuestService guestService, ILogger<GuestController> logger)
        {
            _guestService = guestService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<GuestResponse>>> GetAllGuests()
        {
            _logger.LogInformation("GetAllGuests Started");
            var response = await _guestService.GetAll();
            _logger.LogInformation(_guestService.SerializeMethod(response));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestResponse>> GetGuestById(Guid id)
        {
            _logger.LogInformation("GetGuestById Started");
            var response = await _guestService.Get(id);
            _logger.LogInformation(_guestService.SerializeMethod(response));
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GuestResponse>> AddGuest([FromBody] GuestRequest request)
        {
            _logger.LogInformation("AddGuest Started");
            if (!ModelState.IsValid) { return BadRequest("kindly provide proper data"); }
            var response = await _guestService.Create(request);
            _logger.LogInformation(_guestService.SerializeMethod(response));
            return StatusCode(StatusCodes.Status201Created, response);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<GuestResponse>> Update(Guid id, [FromBody] GuestRequest request)
        //{
        //    if (!ModelState.IsValid) { return BadRequest("kindly provide proper data"); }
        //    var response = await _guestService.Update(id, request);
        //    return Ok(response);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(Guid id)
        //{
        //    await _guestService.Delete(id);
        //    return NoContent();
        //}

        [HttpPut("ADDPhone/{id}")]
        public async Task<ActionResult<GuestResponse>> AddPhone(Guid id, [FromBody] string request)
        {
            _logger.LogInformation("AddPhone Started");
            var response = await _guestService.AddPhone(id, request);
            if (response == null) { return BadRequest("phone number aleady present"); };
            _logger.LogInformation(_guestService.SerializeMethod(response));
            return Ok(response);
        }
    }
}
