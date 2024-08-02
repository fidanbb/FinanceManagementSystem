using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults
{
    public class GetTransactionsByUserIdQueryResult
    {
        public int TransactionID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string FinancialAccountName { get; set; }
        public string TransactionType { get; set; }

        public string OwnerFullName { get; set; }
    }
}
