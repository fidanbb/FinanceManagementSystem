using FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands;
using FinanceManagementSystem.Application.Features.Mediatr.Queries.CategoryQueries;
using FinanceManagementSystem.Application.Features.Mediatr.Queries.FinancialAccountQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]

        public async Task<IActionResult> FinancialAccountsList()
        {
            var values = await _mediator.Send(new GetAllFinancialAccountsQuery());
            return Ok(values);
        }

        [HttpPost]

        public async Task<IActionResult> CreateFinancialAccount(CreateFinancialAccountCommand command)
        {
            await _mediator.Send(command);

            return Ok("Financial Account Created Successfully");
        }

        [HttpGet("GetFinancialAccountById/{id}")]

        public async Task<IActionResult> GetFinancialAccountById(int id)
        {
            var values = await _mediator.Send(new GetFinancialAccountByIdQuery(id));
            return Ok(values);
        }

        [HttpGet("GetFinancialAccountsByUserId/{userId}")]

        public async Task<IActionResult> GetFinancialAccountsByUserId(string userId)
        {
            var values = await _mediator.Send(new GetFinancialAccountsByUserIdQuery(userId));
            return Ok(values);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> RemoveFinancialAccount(int id)
        {
            await _mediator.Send(new RemoveFinancialAccountCommand(id));
            return Ok("Financial Account removed Successfully");
        }

        [HttpPut]

        public async Task<IActionResult> UpdateFinancialAccount(UpdateFinancialAccountCommand command)
        {
            await _mediator.Send(command);
            return Ok("Financial Account updated Successfully");
        }
    }
}
