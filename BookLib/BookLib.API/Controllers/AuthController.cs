using BookLib.Data.ViewModels;
using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginUser loginUser, [FromServices]UserManager<ApplicationUser> userManager)
        {
            var user = userManager.FindByNameAsync(loginUser.Name).Result;
            if (user != null || await userManager.CheckPasswordAsync(user, loginUser.Pass))
            {
                string role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
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
                    access_token = encodedJwt,
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

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUser registerUser, [FromServices]UserManager<ApplicationUser> userManager)
        {
            var oldUser = userManager.FindByNameAsync(registerUser.Name).Result;
            if (oldUser != null)
            {
                ModelState.TryAddModelError("registration_failure", "User with this name already exists.");
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser()
            {
                UserName = registerUser.Name
            };
            var result = await userManager.CreateAsync(user, registerUser.Pass);
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("registrarion_failure", "Unable to create new user.");
                return BadRequest(ModelState);
            }
            result = await userManager.AddToRoleAsync(user, "user");
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("registrarion_failure", "Unable to add user to role.");
                return BadRequest(ModelState);
            }
            return new OkObjectResult("Account created");
        }

        [HttpGet]
        [Authorize]
        public ActionResult<string> Get()
        {
            string res = string.Empty;
            User.Claims.ToList().ForEach(u => res += u.Value + " ");
            return res;
        }

        [Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<string> GetAdm()
        {
            string res = string.Empty;
            User.Claims.ToList().ForEach(u => res += u.Value + " ");
            return res;
        }

        [Route("user")]
        [Authorize(Roles = "user")]
        public ActionResult<string> GetUser()
        {
            string res = string.Empty;
            User.Claims.ToList().ForEach(u => res += u.Value + " ");
            return res;
        }
    }
}
