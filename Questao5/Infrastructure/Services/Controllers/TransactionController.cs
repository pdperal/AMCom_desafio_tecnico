using MediatR;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMediator _mediator;

        public TransactionController(ILogger<TransactionController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("SaveTransaction")]
        public async Task<IActionResult> SaveTransaction(NewTransactionCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result.Data.Id);
            }

            return BadRequest(result.Error);
        }
    }
}