using KiraSoft.Application.Base.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Application.IdentityViewModel
{
    public class UserRoleViewModel : BaseGuidViewModel
    {
        [Required]
        public virtual Guid UserId { get; set; }

        [Required]
        public virtual Guid RoleId { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual RoleViewModel Role { get; set; }
    }
}