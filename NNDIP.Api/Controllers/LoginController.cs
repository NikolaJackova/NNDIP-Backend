using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NNDIP.Api.Dtos.Authentication;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IConfiguration _configuration;
        public LoginController(IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuration = configuration;
        }

        [HttpPost]
        [SwaggerOperation(OperationId = "PostLogin")]
        public async Task<ActionResult<TokenDto>> PostLogin(LoginDto loginDto)
        {
            User user = await _repositoryWrapper.UserRepository.GetByUsernameAndPasswordAsync(loginDto.Username, loginDto.Password);

            if (user != null)
            {
                IEnumerable<UserRole> userRoles = await _repositoryWrapper.UserRepository.GetUserRolesAsync(user.Id);
                var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("UserName", user.Username)
                    };

                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Roles.ToString()));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTCONFIG:SECRET"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JWTCONFIG:ISSUER"],
                    _configuration["JWTCONFIG:AUDIENCE"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(DotNetEnv.Env.GetDouble(_configuration["JWTCONFIG:EXPIRATION"])),
                    signingCredentials: signIn);

                return Ok(new TokenDto { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}
