using Rosered11.OrderService.Application;
using Rosered11.OrderService.Domain;
using Rosered11.OrderService.Domain.Ports.Input.Service;
using Serilog;
// using Serilog.AspNetCore.RequestLoggingOptions

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddScoped<IOrderApplicationService>(x => new OrderApplicationService(null, null));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCustomExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
