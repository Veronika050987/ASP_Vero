using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Movies.Components;
using Movies.Data;
using Movies.Components.Services;
using Movies.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
	options.MultipartBodyLengthLimit = long.MaxValue;
	options.ValueLengthLimit = int.MaxValue;
});

//builder.Services.AddDbContextFactory<MoviesContext>(options =>
//	options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext") ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents(); // Включает интерактивность (кнопки, состояния)

builder.Services.AddSingleton<IMovieService, InMemoryMovieService>(); // Используем InMemoryMovieService

// 2. Регистрация вашего сервиса тем
builder.Services.AddScoped<ThemeService>();

// 3. Регистрация БД (если используете EF Core)
builder.Services.AddDbContextFactory<MoviesContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => {
	// Здесь можно добавить дополнительные настройки Swagger, например:
	//options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Movie API", Version = "v1" });
	//options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { ... }); // Если используете аутентификацию
});

builder.Services.AddEndpointsApiExplorer();
/*/ ---ДОБАВЛЕНИЕ CORS-- */ 	
builder.Services.AddCors(options => {
options.AddPolicy("cors", policy => {
policy.AllowAnyOrigin()
	// Разрешает запросы с любых источников
.AllowAnyMethod()  
	// Разрешает любые HTTP методы (GET, POST, PUT, DELETE и т.д.)
.AllowAnyHeader(); 
	// Разрешает любые HTTP заголовки
});});// --- КОНЕЦ ДОБАВЛЕНИЯ CORS

var app = builder.Build();

// 3. Создание папки для загрузок
var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
if (!Directory.Exists(uploadFolder))
{
	Directory.CreateDirectory(uploadFolder);
}

// 4. Настройка HTTP-конвейера
if (!app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();
/// ---ИСПОЛЬЗОВАНИЕ CORS MIDDLEWARE ---
// Обычно CORS middleware размещают перед middleware, которые могут быть вызваны извне
// (например, контроллеры, статические файлы)
app.UseCors("cors");
// 5. Статические файлы (стандартные + папка Uploads)
app.UseStaticFiles();

// Подключение папки Uploads как отдельного пути
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(uploadFolder),
	RequestPath = "/Uploads"
});

app.MapControllers();

app.UseAntiforgery();

// 5. Маршрутизация компонентов
// App - это ваш главный компонент App.razor, который находится в корне
app.MapRazorComponents<Movies.App>()
	.AddInteractiveServerRenderMode();

app.Run();