using Base.Domain.Common;
using CSharpFunctionalExtensions;

namespace Base.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; init; }
    
    private Email (string value) => Value = value;

    public static Result<Email, ErrorCode> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValid(value))
            return Result.Failure<Email, ErrorCode>(ErrorCode.InvalidEmail);

        return Result.Success<Email, ErrorCode>(new Email(value));
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
    
    // private factory for trusted/already-validated data
    internal static Email FromTrustedSource(string value) => new Email(value);

}
