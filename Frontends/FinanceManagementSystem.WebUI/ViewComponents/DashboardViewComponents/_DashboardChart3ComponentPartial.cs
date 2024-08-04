using FinanceManagementSystem.Dtos.TransactionDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace FinanceManagementSystem.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardChart3ComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _DashboardChart3ComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"Transactions/GetLastFiveMonthsIncome/{userId}");

                if (responseMessage.IsSuccessStatusCode )
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetLastFiveMonthIncomeDto>>(jsonData);

                    return View(values);
                }
            }
            return View();

        }
    }
}
