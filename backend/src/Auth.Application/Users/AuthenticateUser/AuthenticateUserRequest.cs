namespace Auth.Application.Users.AuthenticateUser;

public record AuthenticateUserRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
};