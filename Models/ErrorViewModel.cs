namespace CMV.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = string.Empty; // Initialize to empty string

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
