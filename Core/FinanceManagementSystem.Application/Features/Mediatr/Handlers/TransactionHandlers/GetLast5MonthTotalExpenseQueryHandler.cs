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
    public class GetLast5MonthTotalExpenseQueryHandler : IRequestHandler<GetLast5MonthTotalExpenseQuery, List<GetLast5MonthTotalExpenseQueryResult>>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetLast5MonthTotalExpenseQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<List<GetLast5MonthTotalExpenseQueryResult>> Handle(GetLast5MonthTotalExpenseQuery request, CancellationToken cancellationToken)
        {
            var values = await _transactionReadRepository.GetLastFiveMonthsExpenseAsync(request.UserId);

            return values.Select(x => new GetLast5MonthTotalExpenseQueryResult
            {
                YearMonth = x.FormattedDate,
                TotalExpense = x.TotalExpense,
            }).ToList();
        }
    }
}
