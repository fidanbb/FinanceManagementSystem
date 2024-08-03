using FinanceManagementSystem.Application.Features.Mediatr.Commands.TransactionCommands;
using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.TransactionHandlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand>
    {
        private readonly ITransactionWriteRepository _transactionWriteRepository;

        public CreateTransactionCommandHandler(ITransactionWriteRepository transactionWriteRepository)
        {
            _transactionWriteRepository = transactionWriteRepository;
        }

        public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            await _transactionWriteRepository.CreateTransactionAsync(new Domain.Entities.Transaction
            {
                Amount = request.Amount,
                Description = request.Description,
                TransactionDate=request.TransactionDate,
                CategoryId = request.CategoryId,
                FinancialAccountId = request.FinancialAccountId,
            });
        }
    }
}
