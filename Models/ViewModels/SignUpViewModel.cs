using System.ComponentModel.DataAnnotations;

namespace JobHosting.Models.ViewModels
{
    public class SignUpViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        // ----------------------------------------------------------
        public string JHFullName { get; set; } = string.Empty;
        public string JHResume { get; set; } = string.Empty;
        public string JListerName { get; set; } = string.Empty;
        public string JListerWebsite { get; set; } = string.Empty;
        public string ProfilePhoto { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
