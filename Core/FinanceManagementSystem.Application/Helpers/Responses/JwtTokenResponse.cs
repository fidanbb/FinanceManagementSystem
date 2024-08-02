using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Helpers.Responses
{
    public class JwtTokenResponse
    {
        public JwtTokenResponse(string token, DateTime expiredDate)
        {
            Token = token;
            ExpireDate = expiredDate;
        }

        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
