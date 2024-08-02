using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Domain.Entities
{
    public class FinancialAccount
    {
        public int FinancialAccountID { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
