using DigitalMusic.Application;
using DigitalMusic.Application.Helper;
using DigitalMusic.Persistence;
using DigitalMusic.WebAPI.Extensions;
using DigitalMusic.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Menambahkan logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services dari layer Persistence
builder.Services.ConfigurePersistence(builder.Configuration);

// Add services dari layer Application
builder.Services.ConfigureApplication();

// Add services dari Extension
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

// Add services to the container.
builder.Services.AddControllers();

// Registrasi IHttpClientFactory
builder.Services.AddHttpClient();

// Add services untuk http context
builder.Services.AddHttpContextAccessor();

// ngambil token management dari appseting.json (option pattern)
builder.Services.Configure<TokenManagement>(builder.Configuration.GetSection("TokenManagement"));
var token = builder.Configuration.GetSection("TokenManagement").Get<TokenManagement>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// instal package Microsoft.AspNetCore.Authentication.Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = token.Issuer,
            ValidAudience = token.Audience,
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
        });
});

var app = builder.Build();

// Dapatkan logger untuk program utama
var logger = app.Logger;


try
{
    logger.LogInformation("Aplikasi dimulai.");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // mapping Uploads folder to Resources folder
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.WebRootPath,"uploads")),
        RequestPath = "/Resources"
    });

    app.UseCors();
    
    // jika UseAuthentication dibawah UseAuthorization
    // maka meskipun udah login, ketika hit api yang authorize
    // dia bakalan return 401 (unauthorized)

    // jika UseAuthentication di comment
    // maka meskipun udah login, ketika hit api yang authorize
    // namun bakalan return 401 (unauthorized)

    // Middleware otentikasi JWT
    app.UseAuthentication();

    // jika UseAuthorization di comment
    // maka meskipun udah login, tetap bisa hit api yang authorize
    // namun bakalan error

    // Middleware autorisasi
    app.UseAuthorization();

    app.UseMiddleware<PermisionMiddleware>();
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.LogError(ex, "Aplikasi berhenti karena terjadi kesalahan tidak terduga.");
}
finally
{
    logger.LogInformation("Aplikasi berakhir.");
}


