using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands
{
    public class CreateFinancialAccountCommand:IRequest
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string AppUserId { get; set; }
    }
}
