using System.ComponentModel.DataAnnotations;

namespace JobHosting.Models.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;

        override
        public string ToString()
        {
            return UserName + " " + Password + " " + RememberMe;
        }
    }
}
