using KiraSoft.CrossCutting.Operation.Exceptions.Enum;

namespace KiraSoft.CrossCutting.Operation.Exceptions.Contracts
{
    public interface IBaseException
    {
        FailureCode FailureCode { get; }
    }
}
