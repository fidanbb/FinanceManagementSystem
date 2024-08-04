using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Results.TransactionResults
{
    public class GetLast5MonthTotalExpenseQueryResult
    {
        public string YearMonth { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
