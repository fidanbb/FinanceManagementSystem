using FinanceManagementSystem.Application.Repositories.TransactionRepositories;
using FinanceManagementSystem.Domain.Entities;
using FinanceManagementSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Persistence.Repositories.TransactionRepositories
{
    public class TransactionReadRepository : ReadRepository<Transaction>, ITransactionReadRepository
    {
        public TransactionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
