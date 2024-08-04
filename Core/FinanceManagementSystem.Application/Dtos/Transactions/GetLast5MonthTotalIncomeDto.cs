using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Dtos.Transactions
{
    public class GetLast5MonthTotalIncomeDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public string FormattedDate => new DateTime(Year, Month, 1).ToString("MMM, yyyy");
    }
}
