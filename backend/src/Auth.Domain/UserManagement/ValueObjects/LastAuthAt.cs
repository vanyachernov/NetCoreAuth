namespace Auth.Domain.UserManagement.ValueObjects;

public record LastAuthAt
{
    public LastAuthAt(DateTime date)
    {
        Date = date;
    }
    
    public DateTime Date { get; }
};