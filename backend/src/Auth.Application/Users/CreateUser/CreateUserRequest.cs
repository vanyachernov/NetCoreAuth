using Auth.Application.DTOs;

namespace Auth.Application.Users.CreateUser;

public record CreateUserRequest
{
    public FullNameDto FullNameDto { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
