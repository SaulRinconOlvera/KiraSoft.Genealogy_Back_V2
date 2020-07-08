using KiraSoft.Domain.EntityBase.Contracts;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Domain.Model.Base
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}
