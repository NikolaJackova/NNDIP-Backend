using NNDIP.Api.NNDIPDbContext;
using System.ComponentModel.DataAnnotations;

namespace NNDIP.Api.Dtos.Authentication
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}