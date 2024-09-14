namespace Auth.Domain.UserManagement.ValueObjects;

public record IsDeleted
{
    public IsDeleted(bool status)
    {
        Status = status;
    }
    
    public bool Status { get; }
};