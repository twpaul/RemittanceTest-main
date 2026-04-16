using Microsoft.AspNetCore.Mvc;
using RemittanceTest.Services;

namespace RemittanceTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemittanceController : ControllerBase
    {
        private readonly IRemittanceService _remittanceService;

        public RemittanceController(IRemittanceService remittanceService)
        {
            _remittanceService = remittanceService;
        }

        [HttpPost("{id}/cancel")]
        public IActionResult Cancel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("無效的匯款 ID。請提供正整數。");
            }

            var result = _remittanceService.CancelRemittance(id);

            if (result.ResultType == CancelRemittanceResultType.NotFound)
            {
                return NotFound(result.Message);
            }

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}