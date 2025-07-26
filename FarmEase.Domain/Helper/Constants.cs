namespace FarmEase.Domain.Helper
{
    public class Constants
    {
        public static readonly List<string> ValidExtensions = new()
        {
            ".jpg",".png",".gif",".jpeg"
        };
        public static readonly string PlaceHolderImage = "https://placehold.co/600x400";
        public struct ErrorMessages
        {
            public static readonly string NoRecordFound = "Requested data is not available.";
            public static readonly string NotExists = "No {0} exists with id - {1}.";
            public static readonly string InvalidLogin = "Email or Password is incorrect!";
            public static readonly string UserExists = "User already exists, please login!";
            public static readonly string LoginError = "An error occurred while processing login.";
            public static readonly string RegistrationError = "An error occurred while registering user.";
            public static readonly string UnexpectedError = "An unexpected error occurred.";
            public static readonly string InvalidFile = "The file format is invalid or File size exceeds limit";
            public static readonly string MappingError = "Unable to map {0} with {1}";
            public static readonly string NotAvailable = "Not available";
            public static readonly string InvalidMailInput = "Email, subject, and message must not be empty.";
            public static readonly string MailFailed = "Failed to send email";
            public static readonly string ValidationError = "Mandatory parameter(s) is/are missing - {0}. Please send a valid request.";
        }

        public struct SuccessMessages
        {
            public static readonly string Created = "{0} has been created successfully with id - {1}";
            public static readonly string Updated = "{0} with id - {1} has been updated successfully.";
            public static readonly string Deleted = "{0} with id - {1} has been deleted successfully.";
            public static readonly string MailSent = "An email has been sent to {0} - Check your mailbox";
        }
        public struct Entities
        {
            public static readonly string Amenity = "Amenity";
            public static readonly string Booking = "Booking";
            public static readonly string FarmRoom = "FarmRoom";
            public static readonly string Farm = "Farm";
            public static readonly string User = "User";
        }
        public struct DbSet
        {
            public static readonly string Amenities = "Amenities";
            public static readonly string Bookings = "Bookings";
            public static readonly string FarmRooms = "FarmRooms";
            public static readonly string Farms = "Farms";
            public static readonly string ApplicationUsers = "ApplicationUsers";
        }

        public struct Separator
        {
            public static readonly string Comma = ",";
            public static readonly string Semicolon = ";";
            public static readonly string Pipe = "|";
            public static readonly string Tab = "\t";
            public static readonly string Space = " ";
            public static readonly string Dash = "-";
            public static readonly string Underscore = "_";
            public static readonly string Colon = ":";
            public static readonly string NewLine = "\n";
            public static readonly string CarriageReturnLineFeed = "\r\n";
        }
        public struct BookingInfo
        {
            public static readonly string Subject = "Successfull Booking | FarmEase";
            public const string BookingConfirmationMessage = @"
            <html>
                <body>
                    <h2>Booking Confirmation</h2>
                    <p>Dear {0},</p>
                    <p>Thank you for your booking. Here are your booking details:</p>
                    <table style='border-collapse: collapse; width: 100%;'>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Booking ID</td><td style='padding: 8px; border: 1px solid #ccc;'>{1}</td></tr>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Check-in Date</td><td style='padding: 8px; border: 1px solid #ccc;'>{2}</td></tr>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Check-out Date</td><td style='padding: 8px; border: 1px solid #ccc;'>{3}</td></tr>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Farm</td><td style='padding: 8px; border: 1px solid #ccc;'>{4}</td></tr>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Booking Date</td><td style='padding: 8px; border: 1px solid #ccc;'>{5}</td></tr>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Nights</td><td style='padding: 8px; border: 1px solid #ccc;'>{6}</td></tr>
                        <tr><td style='padding: 8px; border: 1px solid #ccc;'>Total cost</td><td style='padding: 8px; border: 1px solid #ccc;'>{7}</td></tr>
                    </table>
                    <p>Have a nice trip!</p>
                    <br/>
                    <p>Regards,<br/>The FarmEase Team</p>
                </body>
            </html>";
        }

        public struct QueryParameters
        {
            public const string CheckInDate = "checkindate";
            public const string CheckOutDate = "checkoutdate";
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
