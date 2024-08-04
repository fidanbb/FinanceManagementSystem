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
    public class GetTotalIncomeOfTheMonthQueryHandler : IRequestHandler<GetTotalIncomeOfTheMonthQuery, GetTotalIncomeOfTheMonthQueryResult>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;

        public GetTotalIncomeOfTheMonthQueryHandler(ITransactionReadRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<GetTotalIncomeOfTheMonthQueryResult> Handle(GetTotalIncomeOfTheMonthQuery request, CancellationToken cancellationToken)
        {
            var income =await _transactionReadRepository.GetTotalIncomeOfTheMonth(request.UserId,request.Month);

            return new GetTotalIncomeOfTheMonthQueryResult
            {
                Income = income
            };
        }
    }
}
