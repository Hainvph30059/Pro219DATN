namespace Pro219.Web.Constants
{
    public static class Constant
    {
        public const string PageTitleUser = "Adams Store -";
        public const string PageTitleAdmin = "Adams Store Management -";

        public const string TokenNameLocalStorage = "token";
        public const string TokenExpiredLocalStorage = "expired";
        public const string UserInfoLocalStorage = "userInfo";

        public static class Regex
        {
            public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9]).{8,16}$";
            public const string PhoneNumber = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
        }

        public static class MessageValid
        {
            public const string Password = "Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu.";
            public const string Required = "Không được để trống.";
            public const string Max255 = "Tối đa 255 kí tự.";
            public const string Max200 = "Tối đa 200 kí tự.";
            public const string Max100 = "Tối đa 100 kí tự.";
            public const string Min2 = "Tối thiểu 2 kí tự.";
            public const string Email = "Sai định dạng Email.";
            public const string PhoneNumber = "Sai định dạng số điện thoại.";
            public const string PhoneNumberLength = "Tối thiểu 10 số và tối đa 11 số";
        }

        public static class Role
        {
            public const string Admin = "Admin";
            public const string Manager = "Manager";
            public const string Customer = "Customer";
        }

        public static class CascadingNameParams
        {
            public const string UserInfo = "CurrentUserInfo";
        }

        public static class ErrorCode
        {
            public const string EmailOrPhoneAlreadyExit = "email_phone_already_exit";
            public const string OtherError = "other_error";
            public const string EmailOrPhoneRequired = "email_phone_required";
            public const string EmailOrPhoneNotFound = "email_phone_not_found";
        }

        public static readonly Dictionary<string, string> Errors = new Dictionary<string, string>
        {
            { ErrorCode.EmailOrPhoneAlreadyExit, "Email hoặc số điện thoại này đã được sử dụng." },
            { ErrorCode.EmailOrPhoneRequired, "Hãy nhập email của bạn." },
            { ErrorCode.EmailOrPhoneNotFound, "Email không tồn tại trong hệ thống." },
            { ErrorCode.OtherError, "Đã có lỗi xảy ra." },
        };

        public static class ErrorSatusCode
        {
            public const int BadRequest = 400;
            public const int Fobidden = 403;
            public const int NotFound = 404;
            public const int Internal = 500;
        }
    }
}
