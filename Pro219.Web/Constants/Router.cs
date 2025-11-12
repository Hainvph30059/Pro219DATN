namespace Pro219.Web.Constants
{
    public static class RouterConst
    {
        public static class User
        {
            public const string Home = "/";
            public const string SignUp = "/sign-up";
            public const string Login = "/login";
            public const string ForgotPassword = "/forgot-password";
        }

        public static class Admin
        {
            public const string Home = "/admin/home";
            public const string Login = "/admin/login";
            public const string Product = "/admin/product";
            public const string Brand = "/admin/brand";
            public const string Category = "/admin/category";
            public const string Bill = "/admin/bill";
            public const string Statistical = "/admin/statistical";
        }
    }
}
