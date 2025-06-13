using BerrySystem.Core.Commands;
using BerrySystem.Core.Services;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Core.Validation;
using BerrySystem.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateHarvestCommand).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<CreateBerryKindCommand>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddDbContext<BerrySystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLoggerFactory(LoggerFactory.Create(logBuilder => logBuilder.AddSerilog()));
});

builder.Services.AddScoped<DbContext, BerrySystemDbContext>();
builder.Services.AddScoped<IStatisticsHelperService, StatisticsHelperService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMappingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();