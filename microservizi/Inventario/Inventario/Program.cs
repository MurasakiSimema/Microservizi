using Microsoft.EntityFrameworkCore;
using Inventario.Repository;
using Inventario.Repository.Abstraction;
using Inventario.Business;
using Inventario.Business.Abstraction;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

string connectionString = "Server=mssql-server;Database=Inventario;User Id=sa;Password=password123;Encrypt=False";
builder.Services.AddDbContext<InventarioDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();

builder.Services.AddControllers();

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
