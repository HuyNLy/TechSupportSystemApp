using TechSupportSystemApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<TechSupportSystemApp.Services.Interfaces.IEmployeeService, TechSupportSystemApp
.Services.Implementations.EmployeeService>();
builder.Services.AddScoped<TechSupportSystemApp.Services.Interfaces.ITicketService, TechSupportSystemApp
.Services.Implementations.TicketService>();
builder.Services.AddScoped<TechSupportSystemApp.Services.Interfaces.ICategoryService, TechSupportSystemApp
.Services.Implementations.CategoryService>();

builder.Services.AddScoped<ITicketRepo, TicketRepo>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
