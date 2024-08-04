using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults
{
    public class GetLast5MonthTotalIncomeQueryResult
    {
        public string YearMonth { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
