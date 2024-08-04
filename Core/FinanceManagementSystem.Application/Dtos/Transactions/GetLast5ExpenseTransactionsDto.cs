using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Dtos.Transactions
{
    public class GetLast5ExpenseTransactionsDto
    {
        public int TransactionID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
