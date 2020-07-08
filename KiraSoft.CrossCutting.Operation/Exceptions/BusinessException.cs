using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.CrossCutting.Operation.Exceptions.Implementation;

namespace KiraSoft.CrossCutting.Operation.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message, FailureCode failureCode) : base(message, failureCode) { }
    }
}
