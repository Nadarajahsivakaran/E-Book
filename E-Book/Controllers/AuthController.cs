using E_Book.Areas.Identity.Pages.Account;
using E_Book.DataAccess.IRepository;
using E_Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace E_Book.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet("/SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet("/SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            try
            {
                ServiceResponse response = await _authRepository.Login(login);
                if (response.IsSuccess)
                {
                    return RedirectToAction("SignUp");
                }
                return View();
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            try
            {
                ServiceResponse response = await _authRepository.Register(register);
                if (response.IsSuccess)
                {
                    return RedirectToAction("SignIn");   
                }
                return View();
            }
            catch (Exception) {
                return View();
            } 
        }
    }
}
