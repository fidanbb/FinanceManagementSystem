using FinanceManagementSystem.Dtos.TransactionDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace FinanceManagementSystem.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardChart1ComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _DashboardChart1ComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;

            if (token is not null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

                ViewBag.UserId = userId;
                ViewBag.Token = token;
                int month=DateTime.Now.Month-1;

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"Transactions/GetTotalIncomeOfTheMonth?userId={userId}&month={month}");
                var responseMessage2 = await client.GetAsync($"Transactions/GetTotalExpenseOfTheMonth?userId={userId}&month={month}");

                if(responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData=await responseMessage.Content.ReadAsStringAsync();
                    var jsonData2=await responseMessage2.Content.ReadAsStringAsync();

                    var values1 = JsonConvert.DeserializeObject<ResultTotalIncomeAndExpenseDto>(jsonData);
                    var values2 = JsonConvert.DeserializeObject<ResultTotalIncomeAndExpenseDto>(jsonData2);

                    ViewBag.Income = values1.Income;
                    ViewBag.Expense = values2.Expense;
                }
            }
            return View();

        }
    }
}
