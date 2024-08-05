using FinanceManagementSystem.Dtos.Report;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using Razor.Templating.Core;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

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

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"Transactions/GetMonthlyReport?userId={userId}&month={month}");

                if (responseMessage.IsSuccessStatusCode )
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<ResultReportDto>(jsonData);

                    values.Year=DateTime.Now.Year;
                    values.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

                    var htmlContent = await RazorTemplateEngine.RenderAsync("~/Views/Report/Index.cshtml", values);

                    TempData["htmlContent"]=htmlContent;

                    return View(values);
                }
            }

            return View();
        }

        [HttpGet]

        public IActionResult SendReport()
        {
            //var mailBody = TempData["htmlContent"];

            //ViewBag.Report = mailBody;

            return View();
        }

        [HttpPost]


        public  IActionResult SendReport(MailDto mailDto)
        {

            var mailBody = TempData["htmlContent"]?.ToString();
            string pattern = @"<div class=""button-container"">
                <a href=""/Dashboard/Index/"" class=""back"">Back To Dashboard</a>
                <a href=""/Report/SendReport/"" class=""send-button"">Send Report Via Mail</a>
            </div>";

            mailBody = Regex.Replace(mailBody, pattern, string.Empty, RegexOptions.Singleline);

            MimeMessage mimeMessage = new MimeMessage();

           MailboxAddress mailboxAddressFrom = new MailboxAddress("FinanceMS", "bashirova.fidangs@gmail.com");
           mimeMessage.From.Add(mailboxAddressFrom);

           MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailDto.ReceiversMail);
           mimeMessage.To.Add(mailboxAddressTo);

           var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailBody;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

           mimeMessage.Subject = "Monthly Report";

            SmtpClient client = new SmtpClient();
           
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("bashirova.fidangs@gmail.com", "rmnt savk gcat thst");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return RedirectToAction("Index","Dashboard");
        }
    }
}
