using FlightApp.Core;
using FlightApp.Core.Repositories;
using FlightApp.Data;
using FlightApp.Data.Repos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FlightDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),b => b.MigrationsAssembly(typeof(FlightDbContext).Assembly.FullName)));
//builder.Services.AddTransient(typeof(IBaseRepo<>),typeof(BaseRepo<>));
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
