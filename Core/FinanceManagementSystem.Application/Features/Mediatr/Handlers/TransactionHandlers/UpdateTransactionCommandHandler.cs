using FinanceManagementSystem.Application.Features.Mediatr.Commands.TransactionCommands;
using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using FinanceManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.TransactionHandlers
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;
        private readonly ITransactionWriteRepository _transactionWriteRepository;

        public UpdateTransactionCommandHandler(ITransactionWriteRepository transactionWriteRepository, ITransactionReadRepository transactionReadRepository)
        {
            _transactionWriteRepository = transactionWriteRepository;
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var value = await _transactionReadRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.TransactionID == request.TransactionID);

            await _transactionWriteRepository.UpdateTransactionAsync(new Transaction
            {
               TransactionID = value.TransactionID,
               Amount = request.Amount,
               Description = request.Description,
               TransactionDate=value.TransactionDate,
               FinancialAccountId = request.FinancialAccountId,
               CategoryId = request.CategoryId,
            });
        }
    }
}
