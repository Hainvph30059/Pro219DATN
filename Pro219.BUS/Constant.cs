namespace Pro219.API
{
    public class Constant
    {
        public static class ErrorCode
        {
            public const string EmailOrPhoneRequired = "email_phone_required";
            public const string EmailOrPhoneNotFound = "email_phone_not_found";
            public const string EmailOrPhoneAlreadyExit = "email_phone_already_exit";
            public const string CustomerNotFound = "customer_not_found";
            public const string CustomerNotFoundWidthEmailOrPhone = "customer_not_found_with_email_or_phone";
            public const string OtherError = "other_error";
        }
    }
}
