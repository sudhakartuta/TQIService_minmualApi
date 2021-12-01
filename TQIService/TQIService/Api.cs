using DataAccess.Data;
using DataAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace TQIService
{
    public static class Api
    {
        //private static readonly IUserRepository _service;
        //private static readonly ILogger<Api> _logger;


       
        public static void ConfigApi(this WebApplication app)
        {
            app.MapGet("/users",Users);
            app.MapGet("/user/{ID}", User);
            app.MapGet("/login/{UserName}/{Password}", Login);
        }

        public static async Task<IResult> Users(IUserRepository _service)
        {
            try
            {
                var users = await _service.GetAllUsers();
                return Results.Ok(users);
            }
            catch (Exception ex)
            {                
                return Results.Problem(ex.Message);                
            }
        }



        public static async Task<IResult> User(int ID,IUserRepository _service)
        {
            User user = await _service.GetUser(ID);
            if (user == null) return Results.NotFound();
            else return Results.Ok(user);
        }

        public static async Task<IResult> Login(IUserRepository _service,IConfiguration configuration,string UserName, string Password)
        {
            User user=await _service.Login(UserName, Password);
            if (user == null) return Results.NotFound();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Access_level)
            };

            var token = new JwtSecurityToken
            (
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(6),
                signingCredentials:new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key"))),
                    SecurityAlgorithms.HmacSha256
                    )
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(tokenString);

        }
    }
}
