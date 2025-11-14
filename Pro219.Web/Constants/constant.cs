namespace Pro219.Web.Constants
{
    public static class Constant
    {
        public const string PageTitleUser = "Adams Store -";
        public const string PageTitleAdmin = "Adams Store Management -";

        public const string TokenNameLocalStorage = "token";
        public const string TokenExpiredLocalStorage = "expired";

        public class Regex
        {
            public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9]).{8,16}$";
            public const string PhoneNumber = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
        }

        public class MessageValid
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
    }
}
