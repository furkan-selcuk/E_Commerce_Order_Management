using ECommerce.Blazor.Components;
using ECommerce.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDevExpressBlazor();

// Register HttpClient for WebAPI
builder.Services.AddHttpClient("ECommerceApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5055/");
});

// Register Services
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<StokService>();
builder.Services.AddScoped<CariService>();
builder.Services.AddScoped<FaturaService>();
builder.Services.AddScoped<ViewService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
