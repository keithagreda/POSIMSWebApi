namespace POSIMSWebApi.Authentication
{
    public class UserRole
    {
        public const string Admin = "Admin";
        public const string Cashier = "Cashier";
        public const string Inventory = "Inventory";
    }

    public enum UserRoleEnum
    {
        Admin = 0,
        Cashier = 1,
        Inventory = 2,
    }
}
