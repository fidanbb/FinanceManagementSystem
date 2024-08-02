using FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands;
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
    public class RemoveFinancialAccountCommandHandler : IRequestHandler<RemoveFinancialAccountCommand>
    {
        private readonly IFinancialAccountWriteRepository _financialAccountWriteRepository;
        private readonly IFinancialAccountReadRepository _financialAccountReadRepository;

        public RemoveFinancialAccountCommandHandler(IFinancialAccountWriteRepository financialAccountWriteRepository, IFinancialAccountReadRepository financialAccountReadRepository)
        {
            _financialAccountWriteRepository = financialAccountWriteRepository;
            _financialAccountReadRepository = financialAccountReadRepository;
        }
       
        public async Task Handle(RemoveFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var value = await _financialAccountReadRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.FinancialAccountID == request.Id);

           await _financialAccountWriteRepository.RemoveAsync(value);
        }
    }
}
