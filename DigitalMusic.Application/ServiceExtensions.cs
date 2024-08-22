using DigitalMusic.Application.Common.Behaviors;
using DigitalMusic.Application.Helper;
using DigitalMusic.Application.Helper.Interface;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DigitalMusic.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapConfig));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<IAccessTokenHelper, AccessTokenHelper>();
            services.AddScoped<IRefreshTokenHelper, RefreshTokenHelper>();
            services.AddScoped<IVerifyTokenHelper, VerifyTokenHelper>();
            services.AddScoped<IResetPasswordTokenHelper, ResetPasswordTokenHelper>();
            services.AddScoped<ICacheHelper, CacheHelper>();
        }
    }
}