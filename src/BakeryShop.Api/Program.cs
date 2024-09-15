using BakeryShop.Api;
using BakeryShop.Api.Endpoints;
using BakeryShop.Api.Endpoints.Staff;
using BakeryShop.Api.Services;
using BakeryShop.Application;
using BakeryShop.Infrastructure;
using BakeryShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .LoadConfiguration(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebUi();

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var adminInitializer = serviceScope.ServiceProvider.GetRequiredService<AdminInitializer>();
await adminInitializer.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

var apiGroup = app.MapGroup("api");

apiGroup.MapUsers();
apiGroup.MapProducts();
apiGroup.MapCarts();
apiGroup.MapStaff();
apiGroup.MapOrders();

if (app.Environment.IsDevelopment())
{
    apiGroup.MapPost("migrate", async (ApplicationDbContext dbContext) =>
    {
        await dbContext.Database.MigrateAsync();
        await dbContext.SaveChangesAsync();
    });
}

app.Run();