using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries
{
    public class GetLast5MonthTotalExpenseQuery:IRequest<List<GetLast5MonthTotalExpenseQueryResult>>
    {
        public string UserId { get; set; }

        public GetLast5MonthTotalExpenseQuery(string userId)
        {
            UserId = userId;
        }
    }
}
