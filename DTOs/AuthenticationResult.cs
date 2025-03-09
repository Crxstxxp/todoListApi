
namespace todoListApi.DTOs
{
    public class AuthenticationResult
    {
        public string? Token { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}

