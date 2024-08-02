using FinanceManagementSystem.Application.Features.Mediatr.Queries.FinancialAccountQueries;
using FinanceManagementSystem.Application.Features.Mediatr.Results.FinancialAccountResults;
using FinanceManagementSystem.Application.Repositories.FinancialAccountRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.FinancialAccountHandlers
{
    public class GetFinancialAccountsByUserIdQueryHandler : IRequestHandler<GetFinancialAccountsByUserIdQuery, List<GetFinancialAccountsByUserIdQueryResult>>
    {
        private readonly IFinancialAccountReadRepository _readRepository;

        public GetFinancialAccountsByUserIdQueryHandler(IFinancialAccountReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public async Task<List<GetFinancialAccountsByUserIdQueryResult>> Handle(GetFinancialAccountsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var values = _readRepository.Table.Include(x=>x.AppUser)
                                                      .Where(y=>y.AppUserId==request.UserId);

            return values.Select(x=>new GetFinancialAccountsByUserIdQueryResult
            {
                FinancialAccountID=x.FinancialAccountID,
                Name=x.Name,
                Balance=x.Balance,
            }).ToList();
        }
    }
}
