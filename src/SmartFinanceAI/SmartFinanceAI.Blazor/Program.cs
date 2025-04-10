using SmartFinanceAI.Blazor.Components;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAI.Blazor.Data;
using SmartFinanceAI.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<SmartFinanceAIAppContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SmartFinanceAIAppContext") ?? throw new InvalidOperationException("Connection string 'SmartFinanceAIAppContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IInferenceService, CodeRulesInferenceService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
