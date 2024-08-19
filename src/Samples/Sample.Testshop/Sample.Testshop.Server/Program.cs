using Sample.Testshop.Server.Extensions;
using Sample.Testshop.Server.Modules.Checkout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureServices();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/api/orders", CreateOrderHandler.Handle)
.WithName("Create Checkout Order")
.WithOpenApi();

app.MapPatch("/api/orders/{orderId}", UpdateOrderHandler.Handle)
.WithName("Update Checkout Order")
.WithOpenApi();


app.MapGet("/api/orders/{orderId}", GetOrderHandler.Handle)
.WithName("Get Checkout Order")
.WithOpenApi();

app.MapGet("/api/utils/merchants", GetMerchantsHandler.Handle).WithName("Get Available Merchants").WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
