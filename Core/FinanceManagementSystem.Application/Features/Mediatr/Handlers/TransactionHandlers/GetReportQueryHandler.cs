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
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, GetReportQueryResult>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetReportQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }
        public async Task<GetReportQueryResult> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _transactionReadRepository.GetMontlyReport(request.UserId, request.Month);

            return new GetReportQueryResult
            {
                TotalIncome = report.TotalIncome,
                TotalExpense = report.TotalExpense,
                NetSavings = report.NetSavings,
                TransactionCount = report.TransactionCount,
                HighestExpenseAmount = report.HighestExpenseAmount,
                HighestIncomeAmount = report.HighestIncomeAmount,
                SavingsPercentage = (report.NetSavings / report.TotalIncome) * 100,
                LastFiveExpenseTransactions = report.LastFiveExpenseTransactions,
                LastFiveIncomeTransactions = report.LastFiveIncomeTransactions,
                TopFiveTransactions = report.TopFiveTransactions,
            };
        }
    }
}
