using FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands;
using FinanceManagementSystem.Application.Features.Mediatr.Commands.TransactionCommands;
using FinanceManagementSystem.Application.Features.Mediatr.Queries.FinancialAccountQueries;
using FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]

        public async Task<IActionResult> TransactionsList()
        {
            var values = await _mediator.Send(new GetAllTransactionsQuery());
            return Ok(values);
        }

        [HttpPost]

        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            await _mediator.Send(command);

            return Ok("transaction Created Successfully");
        }


        [HttpGet("GetTransactionById/{id}")]

        public async Task<IActionResult> GetTransactionById(int id)
        {
            var values = await _mediator.Send(new GetTransactionByIdQuery(id));
            return Ok(values);
        }

        [HttpGet("GetTransactionsByUserId/{userId}")]

        public async Task<IActionResult> GetTransactionsByUserId(string userId)
        {
            var values = await _mediator.Send(new GetTransactionsByUserIdQuery(userId));
            return Ok(values);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateTransaction(UpdateTransactionCommand command)
        {
            await _mediator.Send(command);
            return Ok("Transaction updated Successfully");
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> RemoveTransaction(int id)
        {
            await _mediator.Send(new RemoveTransactionCommand(id));
            return Ok("Transaction removed Successfully");
        }

        [HttpGet("GetTotalIncomeOfTheMonth")]
        public async Task<IActionResult> GetTotalIncomeOfTheMonth([FromQuery] GetTotalIncomeOfTheMonthQuery request)
        {
            var values = await _mediator.Send(request);
            return Ok(values);
        }

        [HttpGet("GetTotalExpenseOfTheMonth")]
        public async Task<IActionResult> GetTotalExpenseOfTheMonth([FromQuery] GetTotalExpenseOfTheMonthQuery request)
        {
            var values = await _mediator.Send(request);
            return Ok(values);
        }

        [HttpGet("GetLastFiveMonthsIncome/{userId}")]
        public async Task<IActionResult> GetLastFiveMonthsIncome(string userId)
        {
            var values = await _mediator.Send(new GetLast5MonthTotalIncomeQuery(userId));
            return Ok(values);
        }

        [HttpGet("GetLastFiveMonthsExpense/{userId}")]
        public async Task<IActionResult> GetLastFiveMonthsExpense(string userId)
        {
            var values = await _mediator.Send(new GetLast5MonthTotalExpenseQuery(userId));
            return Ok(values);
        }

        [HttpGet("GetMonthlyReport")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] GetReportQuery request)
        {
            var values = await _mediator.Send(request);
            return Ok(values);
        }
    }
}
