namespace AdvFullstack_Labb2.ViewModels.OtherVMs
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ErrorType { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
