using EclipseTasks.Api.Authentication;
using EclipseTasks.Application.Security;
using EclipseTasks.Infrastructure.Registers;
using MediatR.Extensions.FluentValidation.AspNetCore;
using EclipseTasks.Application.Mappers;
using System.Reflection;
using EclipseTasks.Application.Configuration;
using EclipseTasks.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(SimpleAuthenticationOptions.DefaultScheme)
    .AddScheme<SimpleAuthenticationOptions, SimpleAuthenticationHandler>(
        SimpleAuthenticationOptions.DefaultScheme, options => { });

builder.Services.Configure<AppOptions>(builder.Configuration.GetSection("AppSettings"));

var appOptions = new AppOptions();
builder.Configuration.GetSection("AppSettings").Bind(appOptions);

builder.Services.AddRepositories(appOptions);

builder.Services.AddMappings();

var appAssembly = Assembly.Load("EclipseTasks.Application");
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(appAssembly));
builder.Services.AddFluentValidation(new[] { appAssembly });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthentication, SimpleAuthentication>();

builder.Services.AddControllers();

builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();