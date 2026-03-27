using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Components;
using Movies.Data;
using Movies.Components.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents(); // Включает интерактивность (кнопки, состояния)

// 2. Регистрация вашего сервиса тем
builder.Services.AddScoped<ThemeService>();

// 3. Регистрация БД (если используете EF Core)
builder.Services.AddDbContextFactory<MoviesContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Настройка HTTP-конвейера
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Нужно для css, js, картинок в wwwroot
app.UseAntiforgery(); // Обязательно для .NET 8

// 5. Маршрутизация компонентов
// App - это ваш главный компонент App.razor, который находится в корне
app.MapRazorComponents<Movies.App>()
	.AddInteractiveServerRenderMode();

app.Run();