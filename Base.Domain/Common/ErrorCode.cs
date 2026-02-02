namespace Base.Domain.Common
{
    public enum ErrorCode
    {

        #region User
        PasswordNotMatch = 1_000_1,
        UserEmailTaken = 1_000_2,
        #endregion
    }

    public static class ErrorCodeExtensions
    {
        public static string GetMessage(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.PasswordNotMatch => "Passwords do not match",
                ErrorCode.UserEmailTaken => "User email is already taken",
            };
        }
    }
}
