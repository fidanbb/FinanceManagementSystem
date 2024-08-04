using FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.TransactionQueries
{
    public class GetReportQuery:IRequest<GetReportQueryResult>
    {
        public int Month { get; set; }
        public string UserId { get; set; }
    }
}
