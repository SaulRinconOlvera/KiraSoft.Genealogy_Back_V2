using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using Newtonsoft.Json;

namespace KiraSoft.CrossCutting.Operation.Transaction.Implementation
{
    public class SuccessfullyAnswer<T> : ISuccessfullyAnswer<T> where T : class
    {
        public SuccessfullyAnswer() { }
        public SuccessfullyAnswer(T data) { PayLoad = data; Message = "Succesfully executed"; }
        public SuccessfullyAnswer(string mensaje) { Message = mensaje; }
        public SuccessfullyAnswer(string messaje, T data)
        { Message = messaje; PayLoad = data; }

        [JsonProperty("success")]
        public bool Success => true;

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("payLoad")]
        public T PayLoad { get; set; }
    }
}
