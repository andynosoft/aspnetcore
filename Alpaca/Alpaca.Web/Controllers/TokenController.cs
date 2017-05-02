using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Alpaca.Web.Settings;

namespace Alpaca.Web.Controllers {

    [Route("api/[controller]")]
    public class TokenController : Controller {

        private AuthSettings _authSettings;

        public TokenController(IOptions<AuthSettings> authSettings) {
            _authSettings = authSettings.Value;
        }

        [HttpPost]
        public ActionResult Post() {

            var username = Request.Form["username"];
            var password = Request.Form["password"];

            if (username != _authSettings.Username || password != _authSettings.Password) {
                return BadRequest("Invalid username or password.");
            }

            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.TokenSecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: now.AddSeconds(_authSettings.TokenExpiration),
                issuer: _authSettings.TokenIssuer,
                notBefore: now,
                signingCredentials: signingCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new {
                access_token = (string)encodedJwt,
                expires_in = (int)_authSettings.TokenExpiration
            };

            return Json(response);

        }

    }
}
