using CDBCalculation.Application.AppServices;
using CDBCalculation.Application.Interface;
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
builder.Services.AddScoped<ICdbCalculationAppService, CdbCalculationAppService>();
builder.Services.AddScoped<ICdbCalculationService, CdbCalculationService>();
builder.Services.AddScoped<ICdbCalculationValidator, CdbCalculationValidator>();
// Strategies registration  
builder.Services.AddScoped<TaxCalculatorStrategyContext>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Upto6MonthsTaxStrategy>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Upto12MonthsTaxStrategy>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Upto24MonthsTaxStrategy>();
builder.Services.AddScoped<ITaxCalculatorStrategy, Over24MonthsTaxStrategy>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                if (string.IsNullOrWhiteSpace(origin))
                    return false;

                var uri = new Uri(origin);
                return uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
            })
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
