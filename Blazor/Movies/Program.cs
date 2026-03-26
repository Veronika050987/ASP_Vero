using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Components;
using Movies.Data;
using Movies.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
	options.DetailedErrors = true;
});
builder.Services.AddDbContextFactory<MoviesContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем ThemeService
builder.Services.AddScoped<ThemeService>(); // Или AddSingleton, или AddTransient, в зависимости от ваших нужд, но Scoped - хороший выбор для тем.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

// !!! Вот ключевая строка для исправления ошибки !!!
app.MapFallbackToPage("/_Host"); // <-- Указываем на _Host.cshtml в папке Pages

app.Run();