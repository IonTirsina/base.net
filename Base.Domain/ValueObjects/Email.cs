namespace Base.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; init; }
    
    private Email (string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty");
        
        if (!IsValid(value))
            throw new ArgumentException("Email is not valid");

        return new Email(value);
    }

    public static bool IsValid(string value)
    {
        try
        {
            var _ = new System.Net.Mail.MailAddress(value);

            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public override string ToString() => Value;
}
