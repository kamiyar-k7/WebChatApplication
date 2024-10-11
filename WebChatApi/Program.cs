using Application.MapConfig;
using Application.Services.Implentation;
using Application.Services.Interfaces;
using Application.Validation;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Doamin.IRepository.UserPart;
using FluentValidation;
using InfruStructure.Repository.UserPart;
using InfruStructure.WebChatDbContext;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
