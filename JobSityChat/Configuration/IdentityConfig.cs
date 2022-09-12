using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JobSityChat.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection service)
        {

            service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/access-denied";
                    options.LogoutPath = "/logout";
                });
        }
        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {

            app.UseAuthentication();
            app.UseAuthorization();

        }
    }
}
