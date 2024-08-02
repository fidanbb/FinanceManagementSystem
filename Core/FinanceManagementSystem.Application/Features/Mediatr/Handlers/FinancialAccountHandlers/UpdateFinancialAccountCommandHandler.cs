using FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands;
using FinanceManagementSystem.Application.Repositories.FinancialAccountRepositories;
using FinanceManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.FinancialAccountHandlers
{
    public class UpdateFinancialAccountCommandHandler : IRequestHandler<UpdateFinancialAccountCommand>
    {
        private readonly IFinancialAccountWriteRepository _financialAccountWriteRepository;
        private readonly IFinancialAccountReadRepository _financialAccountReadRepository;

        public UpdateFinancialAccountCommandHandler(IFinancialAccountWriteRepository financialAccountWriteRepository, IFinancialAccountReadRepository financialAccountReadRepository)
        {
            _financialAccountWriteRepository = financialAccountWriteRepository;
            _financialAccountReadRepository = financialAccountReadRepository;
        }
        public async Task Handle(UpdateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var value = await _financialAccountReadRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.FinancialAccountID == request.FinancialAccountID);

            await _financialAccountWriteRepository.UpdateAsync(new FinancialAccount
            {
                FinancialAccountID = value.FinancialAccountID,
                Name = request.Name,
                Balance=request.Balance,
                AppUserId = request.AppUserId,
            });
        }
    }
}
