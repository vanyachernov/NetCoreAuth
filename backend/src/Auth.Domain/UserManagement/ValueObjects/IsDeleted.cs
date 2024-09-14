namespace Auth.Domain.UserManagement.ValueObjects;

public record IsDeleted
{
    public IsDeleted(bool isDeleted)
    {
        Status = isDeleted;
    }
    
    public bool Status { get; }
};