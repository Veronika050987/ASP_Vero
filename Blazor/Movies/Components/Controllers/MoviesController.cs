using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;// Для IFormFile
using Microsoft.AspNetCore.Mvc;
using Movies.Components.Services;
using Movies.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace Movies.Components.Controllers
{
		[Route("api/[controller]")] // Определяет базовый путь для всех действий контроллера, например, /api/movies
		[ApiController]// Специальный атрибут для API контроллеров, который включает автоматическую валидацию модели и другие настройки
		public class MoviesController : ControllerBase
		{
			private readonly IMovieService _movieService;
			private readonly IWebHostEnvironment _webHostEnvironment;// Для получения корневого пути приложения
			
			public MoviesController(IMovieService movieService, IWebHostEnvironment webHostEnvironment)
			{
				_movieService = movieService;
				_webHostEnvironment = webHostEnvironment;
			}


			// GET: api/Movies
			[HttpGet]
			public async Task<ActionResult<IEnumerable<Movie>>>GetMovies()
			{
				var movies = await _movieService.GetAllMoviesAsync();
				return Ok(movies);
			}

			// GET: api/Movies/5												
			[HttpGet("{id}")]
			public async Task<ActionResult<Movie>>GetMovie(int id)
			{
				var movie = await _movieService.GetMovieByIdAsync(id);
				
				if(movie == null)
				{
					return NotFound();// Возвращаем 404 Not Found, если фильм не найден
				}
				// Важно: Если ImageUrl в модели - это относительный путь,
				// то в ответе можно вернуть полный URL, чтобы Blazor мог его отобразить.
				// Это делается в сервисе или здесь.

				if (!string.IsNullOrEmpty(movie.ImageUrl) && !movie.ImageUrl.StartsWith("http"))
				{
					movie.ImageUrl = $"{Request.Scheme}://{Request.Host}{movie.ImageUrl}";
				}
				return Ok(movie);
			}

			// POST: api/Movies
			// Принимает данные фильма и файл изображения
			[HttpPost]
			[Consumes("muplipart/form-data")]
			public async Task<ActionResult<Movie>> CreateMovie([FromForm] Movie movieData, IFormFile? file)
			{
				if(!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				// movieData приходит как FromForm, а файл как IFormFile
				// Дополнительная проверка на стороне сервера

				if(file != null && file.Length > 0)
				{
					// Проверка типа файла (опционально, но рекомендуется)
					var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
					var extensions = Path.GetExtension(file.FileName).ToLowerInvariant();
					if(!allowedExtensions.Contains(extensions))
					{
						return BadRequest("Invalid image file type. Allowed types are .jpg, .jpeg, .png, .gif.");
					}
					// Проверка размера файла
					long maxFileSize = 10 * 1024 * 1024;// 10 MB
					
					if(file.Length > maxFileSize)
					{
						return	BadRequest($"File size exceeds the limit of {maxFileSize / (1024 * 1024)} MB.");
					}	
				}

				var createdMovie = await _movieService.AddMovieAsync(movieData, file);

				// Важно: вернуть полный URL к изображению
				if(!string.IsNullOrEmpty(createdMovie.ImageUrl) && !createdMovie.ImageUrl.StartsWith("http"))
				{
					createdMovie.ImageUrl = $"{Request.Scheme}://{Request.Host}{createdMovie.ImageUrl}";
				}
				// Возвращаем 201 Created с ссылкой на созданный ресурс
				return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
			}

			// PUT: api/Movies/5
			// Принимает данные фильма и файл изображения для обновления
			[HttpPut("{id}")]
			[Consumes("multipart/form-data")]
			public async Task<IActionResult>UpdateMovie(int id, [FromForm] Movie updateMoviedata,IFormFile? file)
			{
				if(!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var existingMovie = await _movieService.GetMovieByIdAsync(id);
				
				if(existingMovie == null)
				{
					return NotFound();
				}
				
				if(file != null && file.Length > 0)
				{
					var allowedExtenstions = new[]{ ".jpg", ".jpeg", ".png", ".gif" };

					var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
					if(!allowedExtenstions.Contains(extension))
					{
						return BadRequest("Invalid image file type. Allowed types are .jpg, .jpeg, .png, .gif.");
					}
					
					long maxFileSize = 10 * 1024 * 1024;// 10 MB
					
					if(file.Length > maxFileSize)
					{
						return BadRequest($"File size exceeds the limit of {maxFileSize / (1024 * 1024)} MB.");
					}
				}

				// Сервис должен сам понять, является ли 'updatedMovieData' новыми полями
				// или надо использовать ID из URL для поиска и обновления.
				// В нашем InMemoryMovieService мы передаем ID в метод UpdateMovieAsync.
				var updatedMovie = await _movieService.UpdateMovieAsync(id, updateMoviedata, file);
				
				if (updatedMovie == null)
				{
					return NotFound();
				}

				// Возвращаем полный URL к изображению
				if (!string.IsNullOrEmpty(updatedMovie.ImageUrl) && !updatedMovie.ImageUrl.StartsWith("http"))
				{
					updatedMovie.ImageUrl = $"{Request.Scheme}://{Request.Host}{updatedMovie.ImageUrl}";
				}										
					return Ok(updatedMovie);
				// Возвращаем обновленный объект
			}

			// DELETE: api/Movies/5
			[HttpDelete("{id}")]

			public async Task<IActionResult>DeleteMovie(int id)
			{
				var	deleted = await _movieService.DeleteMovieAsync(id);

				if (!deleted)
				{
					return	NotFound();
				}
	
			return NoContent();
			// Возвращаем 204 No Content, если успешно удалено
			}
		}
}
