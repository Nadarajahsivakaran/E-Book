using E_Book.DataAccess.IRepository;
using E_Book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;

namespace E_Book.DataAccess.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ServiceResponse _serviceResponse;
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthRepository(UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceResponse = new ServiceResponse();
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ServiceResponse> Register(RegisterDTO register)
        {
            try
            {
                ApplicationUser? isEmailExit = await _userManager.FindByEmailAsync(register.Email);

                if (isEmailExit != null)
                {
                    _serviceResponse.IsSuccess = false;
                    _serviceResponse.Result = "The Email already Exist";
                    return _serviceResponse;
                }

                ApplicationUser user = new()
                {
                    Email = register.Email,
                    FirstName = register.FirstName,
                    UserName = register.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    ApplicationUser? newUser = await _applicationDbContext.ApplicationUser
                     .FirstOrDefaultAsync(u => u.UserName == register.Email);

                    string roleName = register.Role.ToString().ToUpper();
                    bool roleExist = await _roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                    {
                        IdentityRole role = new(roleName);
                        await _roleManager.CreateAsync(role);
                    }

                    await _userManager.AddToRoleAsync(newUser, roleName);
                    _serviceResponse.IsSuccess = true;
                }
                return _serviceResponse;
            }
            catch (Exception ex)
            {
                _serviceResponse.IsSuccess = false;
                _serviceResponse.Result = ex;
                return _serviceResponse;
            }
        }


        public async Task<ServiceResponse> Login(LoginDTO login)
        {
            try
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(login.UserName);

                if (user != null)
                {
                    bool isPasswordOk = await _userManager.CheckPasswordAsync(user, login.Password);

                    if (isPasswordOk)
                    {

                        IEnumerable<string> roles = await _userManager.GetRolesAsync(user);
                        user.RoleName = string.Join(", ", roles);

                        _serviceResponse.IsSuccess = true;
                        _serviceResponse.Result = user;
                        return _serviceResponse;
                    }
                }
                _serviceResponse.IsSuccess = false;
                _serviceResponse.Result = "Invalid UserName or Password";
                return _serviceResponse;
            }
            catch (Exception ex)
            {
                _serviceResponse.IsSuccess = false;
                _serviceResponse.Result = ex;
                return _serviceResponse;
            }
        }

    }
}
