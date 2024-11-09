using JobHosting.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace JobHosting.Models
{
    public class UserAccount : IdentityUser
    {
        public string UserType { get; set; } = string.Empty;
        public string JHFullName { get; set; } = string.Empty;
        public string JHResume { get; set; } = string.Empty;
        public string JListerName { get; set; } = string.Empty;
        public string JListerWebsite { get; set; } = string.Empty;
        public string ProfilePhoto { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set;} = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        
        public List<JobListing> Listings { get; set; } = new List<JobListing>();

        public UserAccount()
        {

        }

        public UserAccount(string Email, string UserName, string UserType)
        {
            this.Email = Email;
            this.UserName = UserName;
            this.UserType = UserType;
        }

        public void copyUserViewModel(SignUpViewModel model)
        {
            this.Email = model.Email;
            this.UserName = model.UserName;
            this.UserType = model.UserType;
            this.JHFullName = model.JHFullName;
            this.JHResume = model.JHResume;
            this.ProfilePhoto = model.ProfilePhoto;
            this.City = model.City;
            this.Address = model.Address;
            this.PostalCode = model.PostalCode;
            this.JListerName = model.JListerName;
            this.JListerWebsite = model.JListerWebsite;
        }
        
        override
        public String ToString()
        {
            return $"{Id} {UserName} {UserType} {Email} {Listings}";
        }
    }
}
