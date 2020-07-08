using KiraSoft.Application.Base.ViewModel;
using KiraSoft.Domain.EntityBase.Contracts;
using System.Collections.Generic;

namespace KiraSoft.Application.MapperBase
{
    public interface IGenericMapper<T, TViewModel, TType>
       where T : IBaseEntity<TType> where TViewModel : IBaseViewModel<TType>
    {
        T GetEntity(TViewModel viewModel);
        IEnumerable<T> GetEntities(IEnumerable<TViewModel> viewModels);
        TViewModel GetViewModel(T entity);
        IEnumerable<TViewModel> GetViewModels(IEnumerable<T> entities);
    }
}
