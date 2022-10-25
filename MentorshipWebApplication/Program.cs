using MentorshipWebApplication.Models;
using MentorshipWebApplication.Repository.Data;
using MentorshipWebApplication.Repository.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static NuGet.Packaging.PackagingConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
var logger = new LoggerConfiguration()


        .ReadFrom.Configuration(builder.Configuration)


        .Enrich.FromLogContext()


        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddDbContext<AuditAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbconn") ?? throw new InvalidOperationException("Connection string 'Dbconn' not found.")));
builder.Services.AddControllers();
builder.Services.AddScoped<IAuditRepository, MentorshipWebApplicationBAL.serviceLayer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/validate", [AllowAnonymous] (UserValidationModel request, HttpContext http) =>
{
    if (request.validateCredential(request.userName, request.password))
    {
        
        return new
        {
            IsAuthenticated = true,
        };
    }
    return new
    {
        IsAuthenticated = false,
    };
})
.WithName("Validate");

app.Run();



