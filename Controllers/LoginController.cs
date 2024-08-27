using Back.Auth;
using Back.Db;
using Back.Models;
using front.Utils.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MySqlContext _db;

        public LoginController(MySqlContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Email == login.Email);

                if (user == null)
                {
                    Logger.Instance.Error($"User tried logging in with invalid username or password");
                    return Unauthorized("Invalid e-mail or password");
                }
                if (login.Password != user.Password)
                {
                    Logger.Instance.Error($"User tried logging in with invalid username or password");
                    return Unauthorized("Invalid e-mail or password");
                }

                string accessToken = Jwt.GenerateJwtToken(user.Id, user.Role, tokenType: "access");

                string refreshToken = Jwt.GenerateJwtToken(user.Id, user.Role, tokenType: "refresh");

                int userPlanUsage = _db.UserPlanUsages.FirstOrDefault(x => x.UserId == user.Id).Usages;

                var response = new
                {
                    id = user.Id,
                    email = user.Email,
                    username = user.Username,
                    role = user.Role,
                    accessToken,
                    refreshToken,
                    userPlanUsage
                };

                _db.Users.Where(x => x.Email == login.Email).First().RefreshToken = refreshToken;
                await _db.SaveChangesAsync();

                Logger.Instance.Information("Logged in", response.id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
