using KiraSoft.Application.Base.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Application.IdentityViewModel
{
    public class UserRoleViewModel : BaseViewModel
    {
        [Required]
        public virtual int UserId { get; set; }

        [Required]
        public virtual int RoleId { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual RoleViewModel Role { get; set; }
    }
}