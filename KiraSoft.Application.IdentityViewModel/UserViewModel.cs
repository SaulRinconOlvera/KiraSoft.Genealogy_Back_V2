using KiraSoft.Application.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Application.IdentityViewModel
{
    public class UserViewModel : BaseGuidViewModel
    {
        [StringLength(128)]
        public string PersonName { get; set; }
        [StringLength(128)]
        public string FirstFamilyName { get; set; }
        [StringLength(128)]
        public string SecondFamilyName { get; set; }
        [StringLength(128)]
        public string Alias { get; set; }
        [StringLength(128)]
        public string Avatar { get; set; }
        [StringLength(1024)]
        public string AvatarURL { get; set; }
        public string PhoneNumber { get; set; }
        public string RolesNames { get; set; }
        public string Token { get; set; }
        public string ConfirmationLink { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }


        public virtual ICollection<UserRoleViewModel> Roles { get; set; }
        public virtual ICollection<UserLoginViewModel> Logins { get; set; }
        public virtual ICollection<UserClaimViewModel> Claims { get; set; }
    }
}
