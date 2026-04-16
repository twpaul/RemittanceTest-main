namespace RemittanceTest.Services
{
    public enum CancelRemittanceResultType
    {
        Success,
        NotFound,
        InvalidRequest
    }

    public sealed class CancelRemittanceResult
    {
        public CancelRemittanceResultType ResultType { get; init; }
        public bool IsSuccess => ResultType == CancelRemittanceResultType.Success;
        public string Message { get; init; }

        public CancelRemittanceResult(CancelRemittanceResultType resultType, string message)
        {
            ResultType = resultType;
            Message = message;
        }
    }
}
