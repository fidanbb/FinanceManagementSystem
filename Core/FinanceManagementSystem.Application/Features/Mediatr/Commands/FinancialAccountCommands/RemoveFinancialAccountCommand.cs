using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Commands.FinancialAccountCommands
{
    public class RemoveFinancialAccountCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveFinancialAccountCommand(int id)
        {
            Id = id;
        }
    }
}
