using Application.MapConfig;
using Application.Services.Implentation;
using Application.Services.Interfaces;
using Application.Validation;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Doamin.IRepository.UserPart;
using FluentValidation;
using InfruStructure.Repository.UserPart;
using InfruStructure.WebChatDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


#region DbContext

builder.Services.AddDbContext<ChatDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("ChatConecttionString")));

#endregion

#region Serilog

builder.Host.UseSerilog((context, conf) => conf.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

#endregion

#region Mapper
builder.Services.AddAutoMapper(typeof(MapperConfig));
#endregion


#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

#endregion

#region Services
builder.Services.AddScoped<IUserServices, UserServices>();

#endregion

#region Validator
builder.Services.AddScoped<IValidator<object>, UserDtoValidator>();



#endregion


#region JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true ,
        ValidateLifetime = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audiece"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };

});

#endregion



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






var app = builder.Build();

// Configure the HTTP request pipeline.
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
