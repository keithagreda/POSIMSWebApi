using Domain.ApiResponse;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using POSIMSWebApi.Authentication.Dtos;
using POSIMSWebApi.Authentication.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSIMSWebApi.Authentication.Services
{
    public class UserAuthServices : IUserAuthServices
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private static int _creationIte = 0;
        public UserAuthServices(UserManager<ApplicationIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> RegisterUser(RegisterUserDto register)
        {
            try
            {
                var isExistUser = await _userManager.FindByNameAsync(register.UserName);
                if (isExistUser != null)
                {
                    return "User Already Exists";
                }
                var user = new ApplicationIdentityUser
                {

                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = register.UserName,
                    Email = ""
                };

                //create roles on first run
                if (_creationIte == 0)
                {
                    await CreateRolesAsync();
                }


                var result = await _userManager.CreateAsync(user, register.Password);

                if (!result.Succeeded)
                {
                    //return "Error : Please Try Again";
                    var errors = "";
                    foreach (var error in result.Errors)
                    {
                        errors = error.Code + ", " + error.Description;
                    }

                    return ("Error :" + errors);

                }
                

                if (register.Role == UserRoleEnum.Cashier)
                {
                    if (await _roleManager.RoleExistsAsync(UserRole.Cashier))
                    {
                        await _userManager.AddToRoleAsync(user, UserRole.Cashier);
                    }
                }

                if (register.Role == UserRoleEnum.Inventory)
                {
                    if (await _roleManager.RoleExistsAsync(UserRole.Inventory))
                    {
                        await _userManager.AddToRoleAsync(user, UserRole.Inventory);
                    }
                }

                if (register.Role == UserRoleEnum.Admin)
                {
                    if (await _roleManager.RoleExistsAsync(UserRole.Admin))
                    {
                        await _userManager.AddToRoleAsync(user, UserRole.Admin);
                    }
                }

                return "Success";
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return msg;
            }
        }

        /// <summary>
        /// Role Creation
        /// </summary>
        /// <returns>0 if fail
        /// 1 if success</returns>
        private async Task<int> CreateRolesAsync()
        {
            //to stop redundancy
            if (_creationIte >= 1)
            {
                return 0;
            }
            //check if roles are existing
            //cashier
            if (!await _roleManager.RoleExistsAsync(UserRole.Cashier))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Cashier));
            }
            //inventory
            if (!await _roleManager.RoleExistsAsync(UserRole.Inventory))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Inventory));
            }
            //admin
            if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            }
            _creationIte++;
            return 1;
        }


        public async Task<ApiResponse<UserLoginDto>> LoginAccount(LoginUserDto login)
        {
            try
            {

                var usernameExist = await _userManager.FindByNameAsync(login.UserName);
                var emailExist = await _userManager.FindByEmailAsync(login.UserName);

                var loginUser = new ApplicationIdentityUser();

                if (usernameExist != null)
                {
                    loginUser = usernameExist;
                }
                else if (emailExist != null)
                {
                    loginUser = emailExist;
                }

                if (loginUser == null)
                {
                    return ApiResponse<UserLoginDto>.Fail("No user found. Try again!");
                }

                if(!await _userManager.CheckPasswordAsync(loginUser, login.Password))
                {
                    return ApiResponse<UserLoginDto>.Fail("Wrong password.");
                }

                var generatedToken = new SymmetricSecurityKey(await _userManager.CreateSecurityTokenAsync(loginUser));
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
                var role = "";
                var userRole = await _userManager.GetRolesAsync(loginUser);

                foreach (var userrole in userRole)
                {
                    role = userrole + ",";
                }

                await _userManager.RemoveAuthenticationTokenAsync(loginUser, "userIdentity", role);
                var newRefreshToken = await _userManager.GenerateUserTokenAsync(loginUser, "userIdentity", role);
                await _userManager.SetAuthenticationTokenAsync(loginUser, "userIdentity", role, newRefreshToken);


                var authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,loginUser.Id),
                        new Claim(ClaimTypes.GivenName,""),
                        new Claim(ClaimTypes.Name,loginUser.UserName!),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    };

                foreach (var userrole in userRole)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, userrole));
                }

                var token = new JwtSecurityToken
                    (
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(8),
                        claims: authClaim,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var userInfo = new UserLoginDto
                {
                    UserId = loginUser.Id,
                    UserToken = new JwtSecurityTokenHandler().WriteToken(token),
                    NewRefreshToken = newRefreshToken,
                    UserName = loginUser.UserName!,
                    UserRole = userRole.ToList()
                };

                return ApiResponse<UserLoginDto>.Success(userInfo);


            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return ApiResponse<UserLoginDto>.Fail(message);
            }
        }
    }
}
