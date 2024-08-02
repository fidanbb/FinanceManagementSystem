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
    public class GetAllFinancialAccountsQueryHandler : IRequestHandler<GetAllFinancialAccountsQuery, List<GetAllFinancialAccountsQueryResult>>
    {
        private readonly IFinancialAccountReadRepository _readRepository;

        public GetAllFinancialAccountsQueryHandler(IFinancialAccountReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public async Task<List<GetAllFinancialAccountsQueryResult>> Handle(GetAllFinancialAccountsQuery request, CancellationToken cancellationToken)
        {
            var values = _readRepository.Table.Include(x => x.AppUser);

            return values.Select(x=>new GetAllFinancialAccountsQueryResult
            {
                FinancialAccountID = x.FinancialAccountID,
                Name = x.Name,
                Balance = x.Balance,
                UserFullName=x.AppUser.Name+" "+x.AppUser.Surname
            }).ToList();
        }
    }
}
