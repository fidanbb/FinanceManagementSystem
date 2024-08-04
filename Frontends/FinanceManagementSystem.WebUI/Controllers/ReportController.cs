using FinanceManagementSystem.Dtos.Report;
using FinanceManagementSystem.Dtos.TransactionDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace FinanceManagementSystem.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            int month = id; //id comes as month number

            var token =User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;

            if (token is not null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

                ViewBag.UserId = userId;
                ViewBag.Token = token;
                ViewBag.Month = month;

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"Transactions/GetMonthlyReport?userId={userId}&month={month}");

                if (responseMessage.IsSuccessStatusCode )
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<ResultReportDto>(jsonData);

                  
                    return View(values);
                }
            }

            return View();
        }
    }
}
