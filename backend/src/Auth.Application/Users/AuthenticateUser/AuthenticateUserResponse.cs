namespace Auth.Application.Users.AuthenticateUser;

public record AuthenticateUserResponse
{
    public bool IsAuthSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
};