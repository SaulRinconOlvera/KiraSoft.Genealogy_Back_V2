using KiraSoft.CrossCutting.Operation.Exceptions.Implementation;
using System;

namespace KiraSoft.CrossCutting.Operation.Exceptions
{
    public class ModelStateException : BaseException
    {
        public object ModelState { get; set; }

        public ModelStateException(string message) : base(message, Enum.FailureCode.ModelStateError) { }
        public ModelStateException(object modelState) : base ("Model State Error", Enum.FailureCode.ModelStateError) => ModelState = modelState;
        public ModelStateException(string message, object modelState) : base(message, Enum.FailureCode.ModelStateError) =>
            ModelState = modelState;
    }
}
