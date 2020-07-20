using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KiraSoft.CrossCutting.Operation.Transaction.Implementation
{
    public class ErrorAnswer<T> : IErrorAnswer<T> where T : class
    {
        public ErrorAnswer(string message) { 
            Message = message; PayLoad = null;
            ErrorList = new List<ErrorViewModel>();
        }

        [JsonConstructor]
        private ErrorAnswer() { ErrorList = new List<ErrorViewModel>(); }
        
        public ErrorAnswer(T payload)
        {
            PayLoad = payload;
            Message = "Error at processing request.";
            ErrorList = new List<ErrorViewModel>();
        }

        public ErrorAnswer(string message, T payload)
        {
            PayLoad = payload;
            Message = message;
            ErrorList = new List<ErrorViewModel>();
        }

        public ErrorAnswer(string message, T payload, Exception ex)
        {
            PayLoad = payload;
            Message = message;

            ProcessException(ex);
        }

        public ErrorAnswer(Exception ex) { ProcessException(ex); }

        [JsonProperty("success")]
        public bool Success => false;

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("payLoad")]
        public T PayLoad { get; set; }

        [JsonProperty("errorList")]
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
