namespace Auth.Application.Users.CreateUser;

public record CreateUserResponse
{
    public bool IsSuccessfully { get; set; }
    public IEnumerable<string>? Errors { get; set; }
};