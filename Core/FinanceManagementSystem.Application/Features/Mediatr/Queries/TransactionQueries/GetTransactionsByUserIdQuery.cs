using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries
{
    public class GetTransactionsByUserIdQuery:IRequest<List<GetTransactionsByUserIdQueryResult>>
    {
        public string UserId { get; set; }

        public GetTransactionsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
