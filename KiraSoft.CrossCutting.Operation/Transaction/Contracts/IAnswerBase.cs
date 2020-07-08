namespace KiraSoft.CrossCutting.Operation.Transaction.Contracts
{
    public interface IAnswerBase<T> where T : class
    {
        bool Success { get; }
        string Message { get; }
        T PayLoad { get; }
    }
}
