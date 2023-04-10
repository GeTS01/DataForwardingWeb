namespace DTO
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Pach { get; set; }
        public DateTime thrownAt { get; set; }

        public ErrorResponse(string message, string pach, DateTime thrownAt)
        {
            Message = message;
            Pach = pach;
            this.thrownAt = thrownAt;
        }
    }
}
