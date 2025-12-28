using Stock.Messaging.Extensions;
using Stock.Repositories.Extensions;
using Stock.Services;
using Stock.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMongoDb().AddRepositories().AddServices().AddMessageBus(builder.Configuration);
builder.Services.AddSwaggerGen();

var serviceProvider = builder.Services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
await stockService.InitializeStockAsync();

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