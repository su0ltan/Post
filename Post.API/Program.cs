
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Post.API.Configuration;
using Post.API.Middlewares;
using Post.Application;
using Post.Domain.Entities;
using Post.Infrastructure;
using Post.Infrastructure.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();


builder.Services
       .AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwaggerAuthentication();

app.MapControllers();

app.Run();
