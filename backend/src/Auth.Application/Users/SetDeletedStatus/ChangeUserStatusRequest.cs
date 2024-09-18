using Auth.Application.DTOs;

namespace Auth.Application.Users.SetDeletedStatus;

public record ChangeUserStatusRequest(IsDeletedDto Status);