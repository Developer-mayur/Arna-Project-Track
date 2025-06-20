using Arna_Project_Track.Models;
using System;

namespace Arna_Project_Track.Middlewares
{

    public class UserActivityMiddleware
    {
        private readonly RequestDelegate _next;

        public UserActivityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, MyDBContext dbContext)
        {
            var path = context.Request.Path.Value?.ToLower();

            // ⛔ Skip middleware for login, logout or static files
            if (path != null && (path.Contains("/login") || path.Contains("/account/login") || path.Contains("/logout") || path.Contains(".css") || path.Contains(".js")))
            {
                await _next(context);
                return;
            }

            var userId = context.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var activeUser = dbContext.ActiveUsers.FirstOrDefault(a => a.UserId == userId.Value);

                if (activeUser != null)
                {
                    if (activeUser.LastActiveTime.HasValue &&
                        (DateTime.Now - activeUser.LastActiveTime.Value).TotalMinutes > 10)
                    {
                        activeUser.IsOnline = false;
                    }
                    else
                    {
                        activeUser.IsOnline = true;
                    }

                    activeUser.LastActiveTime = DateTime.Now;

                    dbContext.Update(activeUser);
                    await dbContext.SaveChangesAsync();
                }
            }

            await _next(context); // continue to next middleware or controller
        }

    }
}