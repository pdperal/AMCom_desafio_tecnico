using MediatR;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly ILogger<BalanceController> _logger;
        private readonly IMediator _mediator;

        public BalanceController(ILogger<BalanceController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetBalance(string id)
        {
            var result = await _mediator.Send(new GetBalanceByAccountIdQuery(id));

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Error);
        }
    }
}