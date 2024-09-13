using BakeryShop.Api;
using BakeryShop.Api.Endpoints;
using BakeryShop.Application;
using BakeryShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .LoadConfiguration(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebUi();

var app = builder.Build();

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

app.Run();