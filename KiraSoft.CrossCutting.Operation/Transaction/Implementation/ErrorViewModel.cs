using Newtonsoft.Json;

namespace KiraSoft.CrossCutting.Operation.Transaction.Implementation
{
    public class ErrorViewModel
    {
        //[JsonConstructor]
        //public ErrorViewModel() { }

        public int Number { get; set; }
        public string ErrorType { get; set; }
        public string Message { get; set; }
        public bool HasInnerException { get; set; }
    }
}
