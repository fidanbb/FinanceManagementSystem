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
    public class GetFinancialAccountByIdQueryHandler : IRequestHandler<GetFinancialAccountByIdQuery, GetFinancialAccountByIdQueryResult>
    {
        private readonly IFinancialAccountReadRepository _readRepository;

        public GetFinancialAccountByIdQueryHandler(IFinancialAccountReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public async Task<GetFinancialAccountByIdQueryResult> Handle(GetFinancialAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _readRepository.Table.Include(x=>x.AppUser)
                                                    .FirstOrDefaultAsync(x => x.FinancialAccountID == request.Id);


            return new GetFinancialAccountByIdQueryResult
            {
                FinancialAccountID = value.FinancialAccountID,
                Name = value.Name,
                Balance= value.Balance,
                UserFullName = value.AppUser.Name + " " + value.AppUser.Surname
            };
        }
    }
}
