using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.Service;
using CDBCalculation.Domain.TaxCalculatorStrategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lifetime services registration for Dependency Injection  
builder.Services.AddScoped<CdbCalculationService, CdbCalculationService>();
builder.Services.AddScoped<ICdbCalculationValidator, CdbCalculationValidator>();
// Strategies registration  
builder.Services.AddScoped<ITaxCalculatorStrategy, Upto6MonthsTaxStrategy>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Upto12MonthsTaxStrategy>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Upto24MonthsTaxStrategy>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Over24MonthsTaxStrategy>();

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
