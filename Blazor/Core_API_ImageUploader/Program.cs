using Core_API_ImageUploader.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddCors(options =>
{
	options.AddPolicy("cors", policy =>
	{
		policy.AllowAnyOrigin() // Разрешить запросы с любого источника
			  .AllowAnyMethod() // Разрешить любые HTTP методы (GET, POST, PUT, DELETE и т.д.)
			  .AllowAnyHeader(); // Разрешить любые заголовки
	});
});
// === Конец кода для CORS ===

//builder.Services.AddEndpointsApiExplorer(); // Для .NET 6+
//builder.Services.AddSwaggerGen(options =>
//{
//	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//	{
//		Version = "v1",
//		Title = "Core_API_ImageUploader", // === Название вашего API ===
//		Description = "API for image uploading" // Опционально: описание
//	});

//	// Дополнительные настройки, если нужны (например, для XML-комментариев)
//	// var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
//	// options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

// 1. Обработка статических файлов по умолчанию (из wwwroot)
app.UseStaticFiles();

// 2. Обработка ваших кастомных статических файлов (из папки Files)
app.UseStaticFiles(new StaticFileOptions()
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
	RequestPath = new PathString("/Files")
});


// === Включение middleware для CORS ===
// Важно: эту строку нужно добавить перед UseRouting() или где-то после UseStaticFiles()
// и перед UseEndpoints (который в данном случае представлен MapRazorComponents)
app.UseCors("cors");
// === Конец включения middleware для CORS ===

app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//	options.SwaggerEndpoint("/swagger/v1/swagger.json", "Core_API_ImageUploader v1"); // === Название и версия ===
//																					  // options.RoutePrefix = string.Empty; // Если хотите, чтобы Swagger UI был на корневом URL
//});

app.Run();
