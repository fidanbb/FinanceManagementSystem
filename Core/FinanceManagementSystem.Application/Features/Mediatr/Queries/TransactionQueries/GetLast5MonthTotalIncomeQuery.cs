using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries
{
    public class GetLast5MonthTotalIncomeQuery:IRequest<List<GetLast5MonthTotalIncomeQueryResult>>
    {
        public string UserId { get; set; }

        public GetLast5MonthTotalIncomeQuery(string userId)
        {
            UserId = userId;
        }
    }
}
