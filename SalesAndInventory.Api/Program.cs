using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Data;
using SalesAndInventory.Services;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;
using SalesAndInventory.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("SalesAndInventoryDbConnection");

builder.Services.AddDbContext<SalesAndInventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Register the services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Inicialização do banco de dados para fins de teste
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<SalesAndInventoryDbContext>();
//    DbInitializer.InitializeDatabase(context);
//}

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
