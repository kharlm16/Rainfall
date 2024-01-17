namespace Rainfall.Api.Models.Response
{
    public class Error
    {
        public string Message { get; set; }
        public List<ErrorDetail> Detail { get; set; }
    }
}
