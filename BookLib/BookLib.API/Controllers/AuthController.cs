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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]AuthUser authUser, [FromServices]UserManager<ApplicationUser> userManager)
        {
            var user = userManager.FindByNameAsync(authUser.Name).Result;
            if (user == null || await userManager.CheckPasswordAsync(user, authUser.Pass))
            {
                string role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, authUser.Name),
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
                    username = authUser.Name
                };

                return new OkObjectResult(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            else
            {
                ModelState.TryAddModelError("login_failure", "Invalid username or password.");
                return BadRequest(ModelState);
            }
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
