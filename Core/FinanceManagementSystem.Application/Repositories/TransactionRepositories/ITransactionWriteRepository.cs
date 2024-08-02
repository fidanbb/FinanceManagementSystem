
using FinanceManagementSystem.Domain.Entities;

namespace FinanceManagementSystem.Application.Repositories.TransactionRepositories
{
    public interface ITransactionWriteRepository:IWriteRepository<Transaction>
    {
        Task CreateTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task RemoveTransactionAsync(Transaction transaction);

    }
}
