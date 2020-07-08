using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using System;
using System.Collections.Generic;

namespace KiraSoft.CrossCutting.Operation.Transaction.Implementation
{
    public class ErrorAnswer<T> : IErrorAnswer<T> where T : class
    {
        public ErrorAnswer(string message) { Message = message; PayLoad = null; }
        public ErrorAnswer(T data)
        {
            PayLoad = data;
            Message = "Error at processing request.";
        }
        public ErrorAnswer(string message, T data)
        {
            PayLoad = data;
            Message = message;
        }
        public ErrorAnswer(string message, T data, Exception ex)
        {
            PayLoad = data;
            Message = message;

            ProcessException(ex);
        }
        public ErrorAnswer(Exception ex) { ProcessException(ex); }

        public bool Success => false;

        public string Message { get; private set; }

        public T PayLoad { get; private set; }

        public IList<ErrorViewModel> ErrorList { get; private set; }

        private void ProcessException(Exception ex)
        {
            if (ErrorList is null) ErrorList = new List<ErrorViewModel>();

            ErrorList.Add(new ErrorViewModel()
            {
                Message = ex.Message,
                Number = ex.HResult,
                ErrorType = ex.GetType().Name,
                HasInnerException = ex.InnerException != null
            }); ;

            if (ex.InnerException != null) ProcessException(ex.InnerException);
        }
    }
}
