namespace Auth.Domain.UserManagement.ValueObjects;

public record LastAuthAt
{
    public LastAuthAt(DateTime lastAuthAt)
    {
        Date = lastAuthAt;
    }
    
    public DateTime Date { get; }
};