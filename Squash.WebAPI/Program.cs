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

//var frontUrl = builder.Configuration["Frontend:Url"];

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

// Configure Kestrel for use HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(configureOptions =>
    {
        configureOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls13;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();


app.UseCors(builder =>
    builder.WithOrigins("https://localhost:4200")
           .AllowCredentials()
           .AllowAnyHeader()
           .AllowAnyMethod());

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
    context.Response.Headers.Add("Cross-Origin-Embedder-Policy", "require-corp");
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
