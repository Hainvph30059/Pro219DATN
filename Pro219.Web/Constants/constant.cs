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
        }

        public class MessageValid
        {
            public const string Password = "Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu.";
            public const string Required = "Không được để trống.";
            public const string Max255 = "Tối đa 255 kí tự.";
            public const string Email = "Sai định dạng Email.";
        }
    }
}
