using FinanceManagementSystem.Application.Dtos.Transactions;
using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using FinanceManagementSystem.Domain.Entities;
using FinanceManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Persistence.Repositories.TransactionRepositories
{
    public class TransactionReadRepository : ReadRepository<Transaction>, ITransactionReadRepository
    {
        public TransactionReadRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<GetLast5MonthTotalExpenseDto>> GetLastFiveMonthsExpenseAsync(string userId)
        {
            using var context = new AppDbContext();

            var fiveMonthsAgo = DateTime.Now.AddMonths(-5);

            var expenseReports = await context.Transactions
                .Include(x => x.FinancialAccount)
                .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                            y.CategoryId == 2 &&
                            y.TransactionDate >= fiveMonthsAgo)
                .GroupBy(t => new { t.TransactionDate.Year, t.TransactionDate.Month })
                .Select(g => new GetLast5MonthTotalExpenseDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalExpense = g.Sum(t => t.Amount)
                })
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ToListAsync();

            return expenseReports;
        }

        public async Task<List<GetLast5MonthTotalIncomeDto>> GetLastFiveMonthsIncomeAsync(string userId)
        {
            using var context = new AppDbContext();

            var fiveMonthsAgo = DateTime.Now.AddMonths(-5);

            var incomeReports = await context.Transactions
                .Include(x => x.FinancialAccount)
                .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                            y.CategoryId == 1 && 
                            y.TransactionDate >= fiveMonthsAgo)
                .GroupBy(t => new { t.TransactionDate.Year, t.TransactionDate.Month })
                .Select(g => new GetLast5MonthTotalIncomeDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncome = g.Sum(t => t.Amount)
                })
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ToListAsync();

            return incomeReports;
        }

        public async Task<decimal> GetTotalExpenseOfTheMonth(string userId, int month)
        {
            using var context = new AppDbContext();

            var income = await context.Transactions.Include(x => x.FinancialAccount)
                                                 .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                    y.CategoryId == 2 && y.TransactionDate.Month == month)
                                                 .SumAsync(z => z.Amount);

            return income;
        }

        public async Task<decimal> GetTotalIncomeOfTheMonth(string userId, int month)
        {
            using var context = new AppDbContext();

            var income = await context.Transactions.Include(x => x.FinancialAccount)          
                                                 .Where(y => y.FinancialAccount.AppUser.Id == userId && 
                                                    y.CategoryId==1 && y.TransactionDate.Month==month)
                                                 .SumAsync(z => z.Amount);

            return income;
        }
    }
}
