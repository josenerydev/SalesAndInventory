using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Data;
using SalesAndInventory.Api.Repositories;
using SalesAndInventory.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Adicione o DbContext e configure a string de conex�o
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione o AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Registre os reposit�rios e servi�os gen�ricos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Registre os reposit�rios e servi�os espec�ficos
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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