using System.IdentityModel.Tokens.Jwt;

namespace NNDIP.Api.Dtos.Authentication
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;
    }
}
