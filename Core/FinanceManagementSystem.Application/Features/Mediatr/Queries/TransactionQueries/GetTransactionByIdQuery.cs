using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries
{
    public class GetTransactionByIdQuery:IRequest<GetTransactionByIdQueryResult>
    {
        public int Id { get; set; }

        public GetTransactionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
