using InfruStructure.ChatDbContext;
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
