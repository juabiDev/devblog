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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "devblog_auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
    });

builder.Services.AddAuthorization();

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
app.UseRouting();
app.UseAuthorization();

// Habilitar Swagger en todos los entornos (Azure lo necesita)

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevBlog API v1");
});


// Mapear controladores
app.MapControllers();

app.Run();