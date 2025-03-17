using FibonacciGame.BlazorWebApp.Components;
using FibonacciGame.BusinessLogic.Interfaces;
using FibonacciGame.BusinessLogic.Services;
using FibonacciGame.Common.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Get the config from the appsettings
builder.Services.Configure<GridConfig>(builder.Configuration.GetSection("GridSettings"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add our Fibonacci GameService
builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
