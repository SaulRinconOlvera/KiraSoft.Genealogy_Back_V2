using System;

namespace KiraSoft.Application.Base.ViewModel
{
    public class BaseGuidViewModel : IBaseViewModel<Guid>
    {
        public Guid Id { get; set; }
    }
}
