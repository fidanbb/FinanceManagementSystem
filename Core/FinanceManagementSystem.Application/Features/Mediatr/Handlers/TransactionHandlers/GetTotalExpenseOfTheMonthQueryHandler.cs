using FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries;
using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.TransactionHandlers
{
    public class GetTotalExpenseOfTheMonthQueryHandler : IRequestHandler<GetTotalExpenseOfTheMonthQuery, GetTotalExpenseOfTheMonthQueryResult>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetTotalExpenseOfTheMonthQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }
        public async Task<GetTotalExpenseOfTheMonthQueryResult> Handle(GetTotalExpenseOfTheMonthQuery request, CancellationToken cancellationToken)
        {
            var expense = await _transactionReadRepository.GetTotalExpenseOfTheMonth(request.UserId, request.Month);

            return new GetTotalExpenseOfTheMonthQueryResult
            {
                Expense = expense
            };
        }
    }
}
