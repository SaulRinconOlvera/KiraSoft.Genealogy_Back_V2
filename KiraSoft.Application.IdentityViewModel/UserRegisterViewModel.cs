using KiraSoft.Application.Base.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Application.IdentityViewModel
{
    public class UserRegisterViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FirstFamilyName { get; set; }
        public string SecondFamilyName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string PassConfirmation { get; set; }
        public bool IsGoogleAccount { get; set; }
        public bool IsFacebookAccount { get; set; }
    }
}
