using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Application.Base.ViewModel
{
    public class BaseCatalogViewModel : BaseViewModel
    {
        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }

        [StringLength(20)]
        public virtual string ShortName { get; set; }
    }
}
