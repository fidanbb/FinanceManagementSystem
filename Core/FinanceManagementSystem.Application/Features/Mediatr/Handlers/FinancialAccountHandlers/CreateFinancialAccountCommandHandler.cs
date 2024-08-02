using FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands;
using FinanceManagementSystem.Application.Repositories.FinancialAccountRepositories;
using FinanceManagementSystem.Domain.Entities;
using MediatR;


namespace FinanceManagementSystem.Application.Features.Mediatr.Handlers.FinancialAccountHandlers
{
    public class CreateFinancialAccountCommandHandler : IRequestHandler<CreateFinancialAccountCommand>
    {
        private readonly IFinancialAccountWriteRepository _financialAccountWriteRepository;

        public CreateFinancialAccountCommandHandler(IFinancialAccountWriteRepository financialAccountWriteRepository)
        {
            _financialAccountWriteRepository = financialAccountWriteRepository;
        }

        public async Task Handle(CreateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            await _financialAccountWriteRepository.AddAsync(new FinancialAccount
            {
                Name = request.Name,
                Balance = request.Balance,
                AppUserId = request.AppUserId,
            });
        }
    }
}
