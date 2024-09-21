namespace Auth.Domain.UserManagement.ValueObjects;

public record RegisterAt
{
    public RegisterAt(DateTime date)
    {
        Date = date;
    }
    
    public DateTime Date { get; }
};