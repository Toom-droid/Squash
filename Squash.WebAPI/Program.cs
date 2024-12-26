using Microsoft.EntityFrameworkCore;
using Squash.WebAPI.Repositories;
using Squash.WebAPI.Services;
using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<SquashDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUrlRepository, UrlRepository>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUrlService, UrlService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Squash.WebAPI.Mappings.MappingProfile));

// Add Controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS (Allow only the specific frontend origin in production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.WebHost.UseUrls("http://0.0.0.0:8080");


// Configure Kestrel for use HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Listen on port 8080 for HTTP
    options.ListenAnyIP(8081, listenOptions => listenOptions.UseHttps()); // Listen on port 8081 for HTTPS
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowFrontendOrigin");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
    context.Response.Headers.Add("Cross-Origin-Embedder-Policy", "require-corp");
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();