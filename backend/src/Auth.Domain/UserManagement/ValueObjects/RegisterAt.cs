namespace Auth.Domain.UserManagement.ValueObjects;

public record RegisterAt
{
    public RegisterAt(DateTime registerAt)
    {
        Date = registerAt;
    }
    
    public DateTime Date { get; }
};