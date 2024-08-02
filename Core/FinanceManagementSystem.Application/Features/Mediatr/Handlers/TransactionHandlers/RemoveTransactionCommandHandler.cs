using FinanceManagementSystem.Application.Features.Mediatr.Commands.TransactionCommands;
using FinanceManagementSystem.Application.Repositories.FinancialAccountRepositories;
using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.TransactionHandlers
{
    public class RemoveTransactionCommandHandler : IRequestHandler<RemoveTransactionCommand>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;
        private readonly ITransactionWriteRepository _transactionWriteRepository;
        public RemoveTransactionCommandHandler(ITransactionReadRepository transactionReadRepository, ITransactionWriteRepository transactionWriteRepository)
        {
            _transactionReadRepository = transactionReadRepository;
            _transactionWriteRepository = transactionWriteRepository;
        }
        public async Task Handle(RemoveTransactionCommand request, CancellationToken cancellationToken)
        {
            var value = await _transactionReadRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.TransactionID == request.Id);

            await _transactionWriteRepository.RemoveTransactionAsync(value);
        }
    }
}
