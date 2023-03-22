using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Data;
using SalesAndInventory.Api.Repositories;
using SalesAndInventory.Api.Services;
using SalesAndInventory.Api.Utilities;
using SalesAndInventory.Api.Validators;
using Serilog;
using System.Text.Json;

try
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

    Log.Information("Starting host");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();

    builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
    builder.Services.AddScoped<ISupplierService, SupplierService>();

    builder.Services.AddControllers();

    builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

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

                var result = Result<object>.Failure(errors.Select(e => $"Field {e.Field}: {e.Message}").ToArray());

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        });
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}