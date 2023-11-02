using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersCRUDAPI.DTO;

namespace UsersCRUDAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthJwtController : Controller
    {
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetToken(AuthDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.Name) || string.IsNullOrEmpty(loginDTO.Password))
                return BadRequest("Имя или пароль не совпадает.");

            if (loginDTO.Name.Equals("Mary") && loginDTO.Password.Equals("Komkova"))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("shYCEfQDeKhAkLKmnigpPDDAkD__FdsFbDg"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: "UserTest",
                    audience: "http://localhost:51398",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }
            return Unauthorized();
        }
    }
}
