using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessReportingMVC.Models
{
    [NotMapped]
    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
