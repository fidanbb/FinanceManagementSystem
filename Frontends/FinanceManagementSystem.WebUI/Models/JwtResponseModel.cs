namespace FinanceManagementSystem.WebUI.Models
{
    public class JwtResponseModel
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
