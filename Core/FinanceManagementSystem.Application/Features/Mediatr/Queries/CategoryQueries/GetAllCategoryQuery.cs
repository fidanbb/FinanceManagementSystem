using FinanceManagementSystem.Application.Features.Mediatr.Results.CategoryResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Queries.CategoryQueries
{
    public class GetAllCategoryQuery:IRequest<List<GetAllCategoryQueryResult>>
    {

    }
}
