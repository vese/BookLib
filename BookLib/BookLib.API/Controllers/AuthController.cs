using BookLib.Data.ViewModels;
using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // POST: api/Auth/Login
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _userManager.FindByNameAsync(loginUser.Name).Result;
            if (user != null || await _userManager.CheckPasswordAsync(user, loginUser.Pass))
            {
                string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, loginUser.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    auth_token = encodedJwt,
                    username = loginUser.Name,
                    role = role
                };
                
                return new OkObjectResult(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            else
            {
                ModelState.TryAddModelError("login_failure", "Invalid username or password.");
                return BadRequest(ModelState);
            }
        }

        // POST: api/Auth/Register
        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldUser = _userManager.FindByNameAsync(registerUser.Name).Result;
            if (oldUser != null)
            {
                ModelState.TryAddModelError("Model", "User with this name already exists.");
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser()
            {
                UserName = registerUser.Name
            };
            var result = await _userManager.CreateAsync(user, registerUser.Pass);
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("Model", "Unable to create new user.");
                return BadRequest(ModelState);
            }
            result = await _userManager.AddToRoleAsync(user, "user");
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("Model", "Unable to add user to role.");
                return BadRequest(ModelState);
            }
            return new OkObjectResult(JsonConvert.SerializeObject("Account created", new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        #region Test
        // GET: api/Auth
        [HttpGet]
        [Authorize]
        public ActionResult<string> Get()
        {
            string res = string.Empty;
            User.Claims.ToList().ForEach(u => res += u.Value + " ");
            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Auth/Admin
        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<string> GetAdmin()
        {
            string res = string.Empty;
            User.Claims.ToList().ForEach(u => res += u.Value + " ");
            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Auth/User
        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "user")]
        public ActionResult<string> GetUser()
        {
            string res = string.Empty;
            User.Claims.ToList().ForEach(u => res += u.Value + " ");
            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
        #endregion
    }
}
