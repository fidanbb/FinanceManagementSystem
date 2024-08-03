using FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries;
using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.TransactionHandlers
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdQueryResult>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetTransactionByIdQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<GetTransactionByIdQueryResult> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _transactionReadRepository.Table.Include(x => x.Category)
                                                       .Include(y => y.FinancialAccount)
                                                       .ThenInclude(z => z.AppUser)
                                                       .FirstOrDefaultAsync(a => a.TransactionID == request.Id);

            return new GetTransactionByIdQueryResult
            {
                TransactionID = value.TransactionID,
                CategoryID=value.Category.CategoryID,
                FinancialAccountID=value.FinancialAccount.FinancialAccountID,
                Amount = value.Amount,
                Description = value.Description,
                TransactionDate = value.TransactionDate,
                FinancialAccountName = value.FinancialAccount.Name,
                TransactionType = value.Category.Name,
                OwnerFullName = value.FinancialAccount.AppUser.Name + " " + value.FinancialAccount.AppUser.Surname
            };
        }
    }
}
