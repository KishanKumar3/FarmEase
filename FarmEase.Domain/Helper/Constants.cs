namespace FarmEase.Domain.Helper
{
    public class Constants
    {
        public struct ErrorMessages
        {
            public static readonly string NoRecordFound = "Requested data is not available.";
            public static readonly string InvalidLogin = "Email or Password is incorrect!";
            public static readonly string UserExists = "User already exists, please login!";
            public static readonly string LoginError = "An error occurred while processing login.";
            public static readonly string RegistrationError = "An error occurred while registering user.";
            public static readonly string UnexpectedError = "An unexpected error occurred.";
        }

        public enum ErrorCode
        {
            /// <summary>
            /// Custom Error Code for data not available
            /// </summary>
            Custom = 100,
            /// <summary>
            /// Response Code Bad Request
            /// </summary>
            BadRequest = 400,
            /// <summary>
            /// Response Code Unauthorized
            /// </summary>
            UnAuthorized = 401,
            /// <summary>
            /// Response Code Not Found
            /// </summary>
            NotFound = 404,
            /// <summary>
            /// Response Code Server Error
            /// </summary>
            Problem = 500
        }
    }
}
