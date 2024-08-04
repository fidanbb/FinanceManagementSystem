using FinanceManagementSystem.Application.Dtos.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults
{
    public class GetReportQueryResult
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetSavings { get; set; }
        public decimal HighestIncomeAmount { get; set; }
        public decimal HighestExpenseAmount { get; set; }
        public int TransactionCount { get; set; }

        public decimal SavingsPercentage { get; set; }
        public List<GetLast5IncomeTransactionsDto> LastFiveIncomeTransactions { get; set; }
        public List<GetLast5ExpenseTransactionsDto> LastFiveExpenseTransactions { get; set; }
        public List<TopFiveTransactionsDto> TopFiveTransactions { get; set; }
    }
}
