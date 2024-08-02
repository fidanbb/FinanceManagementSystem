using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Helpers.Responses
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
