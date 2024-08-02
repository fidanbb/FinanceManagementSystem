using FinanceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Commands.TransactionCommands
{
    public class CreateTransactionCommand:IRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public int FinancialAccountId { get; set; }
        public int CategoryId { get; set; }
    }
}
