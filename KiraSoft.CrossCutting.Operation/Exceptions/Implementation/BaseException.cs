using KiraSoft.CrossCutting.Operation.Exceptions.Contracts;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using System;

namespace KiraSoft.CrossCutting.Operation.Exceptions.Implementation
{
    public class BaseException : Exception, IBaseException
    {
        public BaseException(string message, FailureCode failureCode = FailureCode.UnknownError) 
            : base(message) { FailureCode = failureCode;  }
        public FailureCode FailureCode { get; private set; }
    }
}
