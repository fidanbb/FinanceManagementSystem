using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Dtos.TransactionDtos
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public int FinancialAccountId { get; set; }
        public int CategoryId { get; set; }
    }
}
