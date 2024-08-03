using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http;
using FinanceManagementSystem.Dtos.FinancialAccountDtos;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace FinanceManagementSystem.WebUI.Controllers
{
    public class FinancialAccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FinancialAccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                string userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

              
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"FinancialAccounts/GetFinancialAccountsByUserId/{userId}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetFinancialAccountsByUserIdDto>>(jsonData);

                    return View(values);
                }
            }



            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateFinancialAccount()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                string userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                ViewBag.UserId = userId;

            }
             return View();
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> CreateFinancialAccount(CreateFinancialAccountDto createFinancialAccountDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                string userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                ViewBag.UserId = userId;

                createFinancialAccountDto.AppUserId = userId;

                if (!ModelState.IsValid)
                {
                    return View();
                }
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(createFinancialAccountDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("FinancialAccounts", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));

                }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateFinancialAccount(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                string userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                ViewBag.UserId = userId;

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"FinancialAccounts/GetFinancialAccountById/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData=await responseMessage.Content.ReadAsStringAsync();

                    var value=JsonConvert.DeserializeObject<UpdateFinancialAccountDto>(jsonData);

                    value.AppUserId = userId;

                    return View(value);
                }

            }
            return View();
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> UpdateFinancialAccount(UpdateFinancialAccountDto updateFinancialAccountDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                string userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                ViewBag.UserId = userId;

                updateFinancialAccountDto.AppUserId = userId;

                if (!ModelState.IsValid)
                {
                    return View();
                }
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(updateFinancialAccountDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("FinancialAccounts", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));

                }
            }

            return View();
        }



        [Authorize]
        public async Task<IActionResult> RemoveFinancialAccount(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;
            if (token is not null)
            {
              
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"FinancialAccounts/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();

        }

    }
}
