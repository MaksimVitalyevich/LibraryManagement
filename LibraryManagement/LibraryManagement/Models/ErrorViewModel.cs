namespace LibraryManagement.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public object? Code { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
