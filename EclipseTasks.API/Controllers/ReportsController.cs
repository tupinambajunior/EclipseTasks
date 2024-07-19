using EclipseTasks.Application.UseCases.Reports.Productivity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EclipseTasks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "administator")]
        [HttpGet("productivity")]
        public async Task<IActionResult> Productivity()
        {
            var dto = await _mediator.Send(new ReportProductivityQuery(30));

            return Ok(dto);
        }
    }
}