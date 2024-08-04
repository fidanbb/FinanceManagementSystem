using FinanceManagementSystem.Application.Dtos.Report;
using FinanceManagementSystem.Application.Dtos.Transactions;
using FinanceManagementSystem.Domain.Entities;


namespace FinanceManagementSystem.Application.Repositories.TransactionRepositories
{
    public interface ITransactionReadRepository : IReadRepository<Transaction>
    {
        Task<decimal> GetTotalIncomeOfTheMonth(string userId,int month);
        Task<decimal> GetTotalExpenseOfTheMonth(string userId,int month);
        Task<List<GetLast5MonthTotalIncomeDto>> GetLastFiveMonthsIncomeAsync(string userId);
        Task<List<GetLast5MonthTotalExpenseDto>> GetLastFiveMonthsExpenseAsync(string userId);

        Task<GetReportDto> GetMontlyReport(string userId, int month);
    }
}
