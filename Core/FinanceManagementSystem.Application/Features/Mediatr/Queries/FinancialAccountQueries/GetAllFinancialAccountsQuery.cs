using FinanceManagementSystem.Application.Features.Mediatr.Results.FinancialAccountResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.FinancialAccountQueries
{
    public class GetAllFinancialAccountsQuery:IRequest<List<GetAllFinancialAccountsQueryResult>>
    {
    }
}
