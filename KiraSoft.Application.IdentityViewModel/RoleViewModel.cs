using KiraSoft.Application.Base.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Application.IdentityViewModel
{
    public class RoleViewModel : BaseGuidViewModel
    {
        [Required]
        [StringLength(128)]
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public virtual ICollection<UserRoleViewModel> Users { get; set; }
    }
}