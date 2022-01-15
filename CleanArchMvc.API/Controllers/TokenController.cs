using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate;
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        // método é ignorado na documentacao
        [ApiExplorerSettings(IgnoreApi = true)] 
        public async Task<ActionResult<UserTokenModel>> CreateUser([FromBody] LoginModel loginModel)
        {
            var result = await _authenticate.RegisterUser(loginModel.Email, loginModel.Password);
            if (result)
                return Ok($"User {loginModel.Email} was create successfully");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserTokenModel>> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authenticate.Authenticate(loginModel.Email, loginModel.Password);
            if (result)
                return GenerateToken(loginModel);
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attemp.");
                return BadRequest(ModelState);
            }
        }

        private UserTokenModel GenerateToken(LoginModel loginModel)
        {
            //declaracoes do usuário
            var claims = new[]
            {
                new Claim("email", loginModel.Email),
                new Claim("meuvalor","o que voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para gerar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir tempo de expiracao
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserTokenModel()
            {
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
