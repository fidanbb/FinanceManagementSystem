using FinanceManagementSystem.Application.Dtos.User;
using FinanceManagementSystem.Application.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Services.Token
{
    public interface ITokenService
    {
        JwtTokenResponse GenerateJwtToken(CheckAppUserDto result);

    }
}
