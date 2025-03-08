public class RegisterRequest
{
    public required string FullName { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}
