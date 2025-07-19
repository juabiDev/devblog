using DotNetEnv;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;
using Logger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Services.Services;
using Repositories.Repositories;
using RepositoriesContracts.RepositoriesContracts;
using Logger.Logger;
using ServicesContracts.ServicesContracts;
using Repositories.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1. Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddProvider(new FileLoggerProvider("Logs/app.log"));
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Load .env file
Env.Load();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<IJwtService, JwtService>();

// Production environment
// var connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING"); 

// Development environment
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"]
    .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST"))
    .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
    .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
    .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"))
    .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME"));
Console.WriteLine($"Connection string: {connectionString}");
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add Services
// TODO: FIX THIS
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();


var ResiliencePipeline =
       // Retry policy for database operations
       new ResiliencePipelineBuilder()
       .AddRetry(new RetryStrategyOptions
       {
           ShouldHandle = new PredicateBuilder()
                   .Handle<DbUpdateException>()  // Handle DbUpdateException
                   .Handle<SqlException>(), // Handle SqlException
           MaxRetryAttempts = 3, // Retry 3 times
           Delay = TimeSpan.FromSeconds(2), // Delay 2 seconds between retries
           OnRetry = retryArgs =>
           {
               return default;
           }
       })
       .Build();


builder.Services.AddSingleton(ResiliencePipeline);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BlogDbContext>();
    if (context.Database.IsRelational())
    {
        context.Database.Migrate();
    }
}

// Configurar middleware en orden correcto
app.UseHttpsRedirection();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:3000") // o el dominio de tu frontend
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevBlog API v1");
});

// Mapear controladores
app.MapControllers();

app.Run();