using FinanceManagementSystem.Application.Repositories.FinancialAccountRepositories;
using FinanceManagementSystem.Domain.Entities;
using FinanceManagementSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Persistence.Repositories.FinancialAccountRepositories
{
    public class FinancialAccountWriteRepository : WriteRepository<FinancialAccount>, IFinancialAccountWriteRepository
    {
        public FinancialAccountWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
