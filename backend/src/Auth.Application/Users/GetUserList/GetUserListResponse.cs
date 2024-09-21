using Auth.Application.DTOs;

namespace Auth.Application.Users.GetUserList;

public record GetUserListResponse
{
    public Guid Id { get; set; }
    public FullNameDto FullName { get; set; }
    public EmailDto Email { get; set; }
    public RegisterAtDto RegisterAt { get; set; }
    public LastAuthAtDto LastAuthAt { get; set; }
    public IsDeletedDto Status { get; set; }
};