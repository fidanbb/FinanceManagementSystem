using FinanceManagementSystem.Application.Dtos.Report;
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

        public async Task<GetReportDto> GetMontlyReport(string userId, int month)
        {
            using var context = new AppDbContext();

            var expense = await context.Transactions.Include(x => x.FinancialAccount)
                                                 .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                    y.CategoryId == 2 && y.TransactionDate.Month == month)
                                                 .SumAsync(z => z.Amount);

            var income = await context.Transactions.Include(x => x.FinancialAccount)
                                                 .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                    y.CategoryId == 1 && y.TransactionDate.Month == month)
                                                 .SumAsync(z => z.Amount);

            var netSavings = income - expense;

            var last5IncomeTransactions = context.Transactions.Include(x => x.FinancialAccount)
                                                    .Include(a=>a.Category)
                                                 .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                    y.CategoryId == 1 && y.TransactionDate.Month == month)
                                                 .Take(5)
                                                 .OrderByDescending(z => z.TransactionDate);

            var last5ExpenseTransactions = context.Transactions.Include(x => x.FinancialAccount)
                                                 .Include(a => a.Category)
                                              .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                 y.CategoryId == 2 && y.TransactionDate.Month == month)
                                              .Take(5)
                                              .OrderByDescending(z => z.TransactionDate);

            var top5transactions = context.Transactions.Include(x => x.FinancialAccount)
                                                .Include(a => a.Category)
                                             .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                y.TransactionDate.Month == month)
                                             .Take(5)
                                             .OrderByDescending(z => z.Amount);

            var transactionCount=await context.Transactions.Include(x => x.FinancialAccount)
                                             .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                y.TransactionDate.Month == month)
                                             .CountAsync();

            var highestIncomeAmount = await context.Transactions.Include(x => x.FinancialAccount)
                                             .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                 y.CategoryId == 1 &&
                                                y.TransactionDate.Month == month)
                                             .MaxAsync(x => x.Amount);

            var highestExpenseAmount = await context.Transactions.Include(x => x.FinancialAccount)
                                        .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                            y.CategoryId == 2 &&
                                           y.TransactionDate.Month == month)
                                        .MaxAsync(x => x.Amount);



            GetReportDto reportDto = new GetReportDto();

            reportDto.TotalIncome = income;
            reportDto.TotalExpense = expense;
            reportDto.NetSavings = netSavings;
            reportDto.TransactionCount = transactionCount;
            reportDto.HighestIncomeAmount= highestIncomeAmount;
            reportDto.HighestExpenseAmount=highestExpenseAmount;
            reportDto.LastFiveIncomeTransactions = last5IncomeTransactions.Select(x => new GetLast5IncomeTransactionsDto
            {
                TransactionID = x.TransactionID,
                Amount = x.Amount,
                Description = x.Description,
                TransactionType=x.Category.Name,
                TransactionDate = x.TransactionDate,
            }).ToList();

            reportDto.LastFiveExpenseTransactions = last5ExpenseTransactions.Select(x => new GetLast5ExpenseTransactionsDto
            {
                TransactionID = x.TransactionID,
                Amount = x.Amount,
                Description = x.Description,
                TransactionType = x.Category.Name,
                TransactionDate = x.TransactionDate,
            }).ToList();

            reportDto.TopFiveTransactions = top5transactions.Select(x => new TopFiveTransactionsDto
            {
                TransactionID = x.TransactionID,
                Amount = x.Amount,
                Description = x.Description,
                TransactionType = x.Category.Name,
                TransactionDate = x.TransactionDate,
            }).ToList();

            return reportDto;
        }

        public async Task<decimal> GetTotalExpenseOfTheMonth(string userId, int month)
        {
            using var context = new AppDbContext();

            var expense = await context.Transactions.Include(x => x.FinancialAccount)
                                                 .Where(y => y.FinancialAccount.AppUser.Id == userId &&
                                                    y.CategoryId == 2 && y.TransactionDate.Month == month)
                                                 .SumAsync(z => z.Amount);

            return expense;
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
