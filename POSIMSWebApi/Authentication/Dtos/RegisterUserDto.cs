namespace POSIMSWebApi.Authentication.Dtos
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
