using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Helpers.Responses
{
    public class RegisterResponse
    {
        public bool isSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
