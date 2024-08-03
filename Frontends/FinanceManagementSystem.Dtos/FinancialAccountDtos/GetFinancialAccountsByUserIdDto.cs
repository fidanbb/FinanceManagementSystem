using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Dtos.FinancialAccountDtos
{
    public class GetFinancialAccountsByUserIdDto
    {
        public int FinancialAccountID { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string UserFullName { get; set; }
    }
}
