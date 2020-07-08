using KiraSoft.Application.Base.ViewModel;

namespace KiraSoft.Application.IdentityViewModel
{
    public class UserClaimViewModel : BaseViewModel
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int UserId { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}