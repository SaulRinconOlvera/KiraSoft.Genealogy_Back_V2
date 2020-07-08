using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Genealogy.Web.API.Areas.Authentication.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
