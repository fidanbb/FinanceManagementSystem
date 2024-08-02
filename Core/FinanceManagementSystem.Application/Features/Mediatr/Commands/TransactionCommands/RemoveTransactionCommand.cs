using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Features.Mediatr.Commands.TransactionCommands
{
    public class RemoveTransactionCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveTransactionCommand(int id)
        {
            Id = id;
        }
    }
}
