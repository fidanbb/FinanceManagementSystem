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
    public class GetLast5MonthTotalIncomeQueryHandler : IRequestHandler<GetLast5MonthTotalIncomeQuery, List<GetLast5MonthTotalIncomeQueryResult>>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetLast5MonthTotalIncomeQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }
        public async Task<List<GetLast5MonthTotalIncomeQueryResult>> Handle(GetLast5MonthTotalIncomeQuery request, CancellationToken cancellationToken)
        {
            var values = await _transactionReadRepository.GetLastFiveMonthsIncomeAsync(request.UserId);

            return values.Select(x=>new GetLast5MonthTotalIncomeQueryResult
            {
                YearMonth=x.FormattedDate,
                TotalIncome=x.TotalIncome,
            }).ToList();
        }
    }
}
