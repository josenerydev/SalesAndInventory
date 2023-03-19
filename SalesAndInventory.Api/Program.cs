using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Data;
using SalesAndInventory.Api.Repositories;
using SalesAndInventory.Api.Services;
using SalesAndInventory.Api.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Adicione o DbContext e configure a string de conexão
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione o AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Registre os repositórios e serviços genéricos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Registre os repositórios e serviços específicos
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();

//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = errorFeature.Error;

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var errors = validationException.Errors.Select(x => new
            {
                Field = x.PropertyName,
                Message = x.ErrorMessage
            });

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                Message = "Validation errors occurred",
                Errors = errors
            }));
        }
    });
});

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