using CleanCode.Application.Application;
using CleanCode.Domain.Database.MongoFactory;
using CleanCode.Infrastructure.Repository;
using CleanCode.Interface.Application;
using CleanCode.Interface.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure Database
builder.Services.Configure<MongoClientFactory>(builder.Configuration.GetSection("Mongo"));

// Add services to the container.
builder.Services.AddScoped<IUserApplication, UserApplication>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
