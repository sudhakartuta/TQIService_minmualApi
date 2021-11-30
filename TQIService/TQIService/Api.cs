using DataAccess.Data;
using DataAccess.Repositories;

namespace TQIService
{
    public static class Api
    {
        //private static readonly IUserRepository _service;
        //private static readonly ILogger<Api> _logger;


       
        public static void ConfigApi(this WebApplication app)
        {
            app.MapGet("/users", Users);
            app.MapGet("/user/{ID}", User);
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
    }
}
