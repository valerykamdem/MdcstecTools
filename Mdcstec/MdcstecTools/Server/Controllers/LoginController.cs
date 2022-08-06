using MdcstecTools.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MdcstecTools.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IConfiguration configuration,
                               SignInManager<IdentityUser> signInManager,
                               ILogger<LoginController> logger)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                //if (!result.Succeeded) return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });

                if (login.Email != "admin@app.com" || login.Password != "Admin1#")
                {
                    return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });
                }
                else
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, login.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expiry = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["JwtConfig:ExpiryTimeFrame"]));

                    var token = new JwtSecurityToken(
                        _configuration["JwtConfig:Issuer"],
                        _configuration["JwtConfig:Audience"],
                        claims,
                        expires: expiry,
                        signingCredentials: creds
                    );

                    return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }
            return BadRequest(false);
        }
    }
}
