using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Tools
{
    public class JwtTokenDefaults
    {
        //public const string ValidAudience = "https://localhost";
        //public const string ValidIssuer = "https://localhost";
        public const string ValidAudience = "http://fidanfidanfidan-001-site1.ktempurl.com/";
        public const string ValidIssuer = "http://fidanfidan-001-site1.atempurl.com";
        public const string Key = "FinanceManagementSystem+*010203CFINANCE01+*..020304FinanceManagementSystem";
        public const int Expire = 5;
    }
}
