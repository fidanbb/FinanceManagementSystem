using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Dtos.TransactionDtos
{
    public class GetLastFiveMonthIncomeDto
    {
        public string YearMonth { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
