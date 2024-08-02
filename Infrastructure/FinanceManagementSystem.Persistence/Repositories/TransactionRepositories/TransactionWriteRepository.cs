using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using FinanceManagementSystem.Domain.Entities;
using FinanceManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagementSystem.Persistence.Repositories.TransactionRepositories
{
    public class TransactionWriteRepository : WriteRepository<Transaction>, ITransactionWriteRepository
    {
        public TransactionWriteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            using var context = new AppDbContext();

            await context.Transactions.AddAsync(transaction);

            var value = await context.FinancialAccounts.FirstOrDefaultAsync(x => x.FinancialAccountID == transaction.FinancialAccountId);

            decimal newBalance = 0;

            if (transaction.CategoryId == 1) //if income
            {
                newBalance = value.Balance +transaction.Amount;
                value.Balance = newBalance;
            }

            else if (transaction.CategoryId == 2) //if expense
                                                  
            {
                newBalance = value.Balance - transaction.Amount;
                value.Balance = newBalance;

            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveTransactionAsync(Transaction transaction)
        {
            using var context = new AppDbContext();

            context.Transactions.Remove(transaction);

            var value = await context.FinancialAccounts.FirstOrDefaultAsync(x => x.FinancialAccountID == transaction.FinancialAccountId);

            value.Balance -= transaction.Amount;

            await context.SaveChangesAsync();

        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            using var context = new AppDbContext();


            var oldTransaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.TransactionID == transaction.TransactionID);
          
                var oldFinancialAccount = await context.FinancialAccounts.FirstOrDefaultAsync(x => x.FinancialAccountID == oldTransaction.FinancialAccountId);

                decimal oldBalance = 0;

                if (oldTransaction.CategoryId == 1)
                {
                    oldBalance = oldFinancialAccount.Balance - oldTransaction.Amount;
                    oldFinancialAccount.Balance = oldBalance;
                }

                else if (oldTransaction.CategoryId == 2)
                {
                    oldBalance = oldFinancialAccount.Balance + oldTransaction.Amount;
                    oldFinancialAccount.Balance = oldBalance;
                }


            context.Transactions.Update(transaction);


            var value = await context.FinancialAccounts.FirstOrDefaultAsync(x => x.FinancialAccountID == transaction.FinancialAccountId);

            decimal newBalance = 0;

            if (transaction.CategoryId == 1) //if income
            {
                newBalance = value.Balance + transaction.Amount;
                value.Balance = newBalance;
            }

            else if (transaction.CategoryId == 2) //if expense

            {
                newBalance = value.Balance - transaction.Amount;
                value.Balance = newBalance;

            }

            await context.SaveChangesAsync();
        }
    }
}
