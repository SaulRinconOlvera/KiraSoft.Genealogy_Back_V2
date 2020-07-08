using KiraSoft.CrossCutting.Operation.Transaction.Contracts;

namespace KiraSoft.CrossCutting.Operation.Transaction.Implementation
{
    public class SuccessfullyAnswer<T> : ISuccessfullyAnswer<T> where T : class
    {
        public SuccessfullyAnswer() { }
        public SuccessfullyAnswer(T data) { PayLoad = data; Message = "Succesfully executed"; }
        public SuccessfullyAnswer(string mensaje) { Message = mensaje; }
        public SuccessfullyAnswer(string messaje, T data)
        { Message = messaje; PayLoad = data; }


        public bool Success => true;

        public string Message { get; private set; }

        public T PayLoad { get; private set; }
    }
}
