namespace Pro219.Web.Constants
{
    public static class RouterConst
    {
        public const string AccessDenied = "/access-denined";
        public const string NotFound = "/not-found";
        public static class User
        {
            // Home
            public const string Home = "/";

            // Auth
            public const string SignUp = "/sign-up";
            public const string Login = "/login";
            public const string ForgotPassword = "/forgot-password";
            public const string ChangePassword = "/change-password";
        }

        public static class Admin
        {
            // Root
            public const string Root = "/admin/";

            // Home
            public const string Home = "/admin/home";

            // Auth
            public const string Login = "/admin/login";

            // Product
            public const string Product = "/admin/products";
            public const string CreateProduct = "/admin/products/create";
            public const string EditProduct = "/admin/products/edit";

            // Brand
            public const string Brand = "/admin/brands";
            public const string CreateBrand = "/admin/brands/create";
            public const string EditBrand = "/admin/brands/edit";

            // Category
            public const string Category = "/admin/categories";
            public const string CreateCategory = "/admin/categories/create";
            public const string EditCategory = "/admin/categories/edit";

            // Size
            public const string Size = "/admin/sizes";
            public const string CreateSize = "/admin/sizes/create";
            public const string EditSize = "/admin/sizes/edit";

            // Color
            public const string Color = "/admin/colors";
            public const string CreateColor = "/admin/colors/create";
            public const string EditColor = "/admin/colors/edit";

            // Sale
            public const string Sale = "/admin/sales";
            public const string CreateSale = "/admin/sales/create";
            public const string EditSale = "/admin/sales/edit";

            // Coupon
            public const string Coupon = "/admin/coupons";
            public const string CreateCoupon = "/admin/coupons/create";
            public const string EditCoupon = "/admin/coupons/edit";

            // User
            public const string User = "/admin/users";
            public const string CreateUser = "/admin/users/create";
            public const string EditUser = "/admin/users/edit";

            // Customer
            public const string Customer = "/admin/customers";
            public const string CreateCustomer = "/admin/customers/create";
            public const string EditCustomer = "/admin/customers/edit";

            // Bill
            public const string Bill = "/admin/bills";

            // Statistical
            public const string Statistical = "/admin/statistical";
        }
    }
}
