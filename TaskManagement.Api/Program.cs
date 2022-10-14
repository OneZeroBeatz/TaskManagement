using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TaskManagement.Api;
using TaskManagement.Api.Extensions;
using TaskManagement.Api.Services;
using TaskManagement.Application.Factories;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.MessageHandlers.Users;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Application.Vaidations.Users;
using TaskManagement.Infrastructure.Configurations;
using TaskManagement.Infrastructure.DataAccess;
using TaskManagement.Infrastructure.DataAccess.Repositories;
using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TaskManagementDbContext>(options =>
{
    options.UseSqlServer(dbConnectionString);
});

builder.Services.AddHangfire(x => x.UseInMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task management documentation",
        Description = "Api for managing tasks that placed in lists managing Tasks",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(builder.Configuration.GetSection("Mail").Get<MailConfiguration>());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDailyListRepository, DailyListRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IAuthenticationTokenFactory, AuthenticationTokenFactory>();
builder.Services.AddScoped<IGetTasksForDailyListResponseFactory, GetTasksForDailyListResponseFactory>();
builder.Services.AddScoped<IGetDailyListsResponseFactory, GetDailyListsResponseFactory>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserNotificationService, UserNotificationService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddHostedService<HostedService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token:Key").Value)),
    };
});


var messageHandlersAssembly = Assembly.GetAssembly(typeof(LoginCommandHandler));
builder.Services.AddMediatR(messageHandlersAssembly!);
builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.ConfigureExceptionHandler();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseUserPersistenceCheck();

app.MapControllers();

app.Run();
