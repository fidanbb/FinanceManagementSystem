using FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries;
using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
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
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<GetAllTransactionsQueryResult>>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetAllTransactionsQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<List<GetAllTransactionsQueryResult>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var values = _transactionReadRepository.Table.Include(x => x.Category)
                                                        .Include(y => y.FinancialAccount)
                                                        .ThenInclude(z => z.AppUser);

            return values.Select(x=> new GetAllTransactionsQueryResult
            {
                TransactionID = x.TransactionID,
                Amount = x.Amount,
                Description = x.Description,
                TransactionDate = x.TransactionDate,
                FinancialAccountName=x.FinancialAccount.Name,
                TransactionType=x.Category.Name,
                OwnerFullName=x.FinancialAccount.AppUser.Name + " "+x.FinancialAccount.AppUser.Surname
            }).ToList();
                                                        
        }
    }
}
