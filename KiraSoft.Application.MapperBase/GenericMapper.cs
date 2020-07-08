using AutoMapper;
using KiraSoft.Application.Base.ViewModel;
using KiraSoft.Domain.EntityBase.Contracts;
using System.Collections.Generic;

namespace KiraSoft.Application.MapperBase
{
    public class GenericMapper<T, TViewModel, TType> : IGenericMapper<T, TViewModel, TType>
        where T : IBaseEntity<TType> where TViewModel : IBaseViewModel<TType>
    {
        private readonly IMapper _mapper;

        public GenericMapper(IMapper mapper) => _mapper = mapper;

        public IEnumerable<T> GetEntities(IEnumerable<TViewModel> viewModels) =>
            _mapper.Map<IEnumerable<T>>(viewModels);

        public T GetEntity(TViewModel viewModel) => _mapper.Map<T>(viewModel);
        public TViewModel GetViewModel(T entity) => _mapper.Map<TViewModel>(entity);

        public IEnumerable<TViewModel> GetViewModels(IEnumerable<T> entities) =>
            _mapper.Map<IEnumerable<TViewModel>>(entities);
    }
}
