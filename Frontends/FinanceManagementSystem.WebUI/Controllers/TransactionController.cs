using FinanceManagementSystem.Dtos.CategoryDtos;
using FinanceManagementSystem.Dtos.FinancialAccountDtos;
using FinanceManagementSystem.Dtos.TransactionDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace FinanceManagementSystem.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionController(IHttpClientFactory httpClientFactory)
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
                var responseMessage = await client.GetAsync($"Transactions/GetTransactionsByUserId/{userId}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetTransactionsByUserId>>(jsonData);

                    return View(values);
                }
            }

            return View();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateTransaction()
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
                var responseMessage = await client.GetAsync($"Categories");
                var responseMessage2 = await client.GetAsync($"FinancialAccounts/GetFinancialAccountsByUserId/{userId}");

                if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetAllCategoriesDto>>(jsonData);
                    var values2 = JsonConvert.DeserializeObject<List<GetFinancialAccountsByUserIdDto>>(jsonData2);

                    List<SelectListItem> categories = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Name,
                                                        Value = x.CategoryID.ToString()
                                                    }).ToList();

                    List<SelectListItem> financialAccounts = (from x in values2
                                                       select new SelectListItem
                                                       {
                                                           Text = x.Name,
                                                           Value = x.FinancialAccountID.ToString()
                                                       }).ToList();

                    ViewBag.Categories = categories;
                    ViewBag.FinancialAccounts = financialAccounts;
                }

            }
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionDto createTransactionDto)
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
                var responseMessage = await client.GetAsync($"Categories");
                var responseMessage2 = await client.GetAsync($"FinancialAccounts/GetFinancialAccountsByUserId/{userId}");

                if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetAllCategoriesDto>>(jsonData);
                    var values2 = JsonConvert.DeserializeObject<List<GetFinancialAccountsByUserIdDto>>(jsonData2);

                    List<SelectListItem> categories = (from x in values
                                                       select new SelectListItem
                                                       {
                                                           Text = x.Name,
                                                           Value = x.CategoryID.ToString()
                                                       }).ToList();

                    List<SelectListItem> financialAccounts = (from x in values2
                                                              select new SelectListItem
                                                              {
                                                                  Text = x.Name,
                                                                  Value = x.FinancialAccountID.ToString()
                                                              }).ToList();

                    ViewBag.Categories = categories;
                    ViewBag.FinancialAccounts = financialAccounts;
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var jsonData3 = JsonConvert.SerializeObject(createTransactionDto);
                StringContent stringContent = new StringContent(jsonData3, Encoding.UTF8, "application/json");
                var responseMessage3 = await client.PostAsync("Transactions", stringContent);
                if (responseMessage3.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));

                }

            }
            return View();
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateTransaction(int id)
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
                var responseMessage = await client.GetAsync($"Categories");
                var responseMessage2 = await client.GetAsync($"FinancialAccounts/GetFinancialAccountsByUserId/{userId}");
                var responseMessage3 = await client.GetAsync($"Transactions/GetTransactionById/{id}");

                if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode && responseMessage3.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                    var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetAllCategoriesDto>>(jsonData);
                    var values2 = JsonConvert.DeserializeObject<List<GetFinancialAccountsByUserIdDto>>(jsonData2);
                    var values3 = JsonConvert.DeserializeObject<UpdateTransactionDto>(jsonData3);

                    List<SelectListItem> categories = (from x in values
                                                       select new SelectListItem
                                                       {
                                                           Text = x.Name,
                                                           Value = x.CategoryID.ToString()
                                                       }).ToList();

                    List<SelectListItem> financialAccounts = (from x in values2
                                                              select new SelectListItem
                                                              {
                                                                  Text = x.Name,
                                                                  Value = x.FinancialAccountID.ToString()
                                                              }).ToList();

                    ViewBag.Categories = categories;
                    ViewBag.FinancialAccounts = financialAccounts;

                    return View(values3);
                }

            }
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateTransaction(UpdateTransactionDto updateTransactionDto)
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
                var responseMessage = await client.GetAsync($"Categories");
                var responseMessage2 = await client.GetAsync($"FinancialAccounts/GetFinancialAccountsByUserId/{userId}");

                if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<List<GetAllCategoriesDto>>(jsonData);
                    var values2 = JsonConvert.DeserializeObject<List<GetFinancialAccountsByUserIdDto>>(jsonData2);

                    List<SelectListItem> categories = (from x in values
                                                       select new SelectListItem
                                                       {
                                                           Text = x.Name,
                                                           Value = x.CategoryID.ToString()
                                                       }).ToList();

                    List<SelectListItem> financialAccounts = (from x in values2
                                                              select new SelectListItem
                                                              {
                                                                  Text = x.Name,
                                                                  Value = x.FinancialAccountID.ToString()
                                                              }).ToList();

                    ViewBag.Categories = categories;
                    ViewBag.FinancialAccounts = financialAccounts;

                }

                if (!ModelState.IsValid)
                {
                    return View();
                }


                var jsonData3 = JsonConvert.SerializeObject(updateTransactionDto);
                StringContent stringContent = new StringContent(jsonData3, Encoding.UTF8, "application/json");
                var responseMessage3 = await client.PutAsync("Transactions", stringContent);
                if (responseMessage3.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }


            }
            return View();
        }



        [Authorize]
        public async Task<IActionResult> RemoveTransaction(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "financemstoken")?.Value;
            if (token is not null)
            {

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"Transactions/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();

        }

    }
}
