namespace DreamEducation.Domain.Commons
{
    public class ErrorResponse
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public ErrorResponse(int? code = null, string message = null)
        {
            Code = code;
            Message = message;
        }
    }
}
