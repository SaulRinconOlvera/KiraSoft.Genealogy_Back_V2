using KiraSoft.CrossCutting.Operation.Transaction.Implementation;
using System.Collections.Generic;

namespace KiraSoft.CrossCutting.Operation.Transaction.Contracts
{
    public interface IErrorAnswer<T> : IAnswerBase<T> where T : class
    {
        IList<ErrorViewModel> ErrorList { get; }
    }
}
