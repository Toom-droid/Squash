using Microsoft.EntityFrameworkCore;
using Squash.WebAPI.Repositories;
using Squash.WebAPI.Services;
using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<SquashDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

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

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
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
    app.UseHttpsRedirection(); 
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
