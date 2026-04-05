using Movies.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Components.Services
{
	public class InMemoryMovieService : IMovieService
	{
		// Имитация базы данных в памяти

		private static readonly List<Movie> _movies = new List<Movie>();
		private static int _nextId = 1;

		// Путь для сохранения изображений. В реальном приложении это может быть конфигурируемо.
		private readonly string _imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(),
			"Uploads", "MovieImages");

		public InMemoryMovieService()
		{
			// Создаем папку для изображений, если она не существует
			if(!Directory.Exists(_imageUploadPath))
			{
				Directory.CreateDirectory(_imageUploadPath);
			}
		}

		public Task<IEnumerable<Movie>>GetAllMoviesAsync()
		{
			// Возвращаем копию списка, чтобы предотвратить изменение извне
			return Task.FromResult<IEnumerable<Movie>>(_movies.ToList());
		}

		public Task<Movie?>GetMovieByIdAsync(int id)
		{
			var movie = _movies.FirstOrDefault(m => m.Id == id);
			return Task.FromResult(movie);
		}

		public async Task<Movie> AddMovieAsync(Movie movie, IFormFile? imageFile)
		{

			// Если есть изображение, сохраняем его
			if(imageFile != null && imageFile.Length > 0)
			{
				movie.ImageUrl = await SaveImageAsync(imageFile);
			}
			movie.Id = _nextId++;
			_movies.Add(movie);
			return movie;
		}

		public async Task<Movie> UpdateMovieAsync(int id, Movie updatedMovie, IFormFile? imageFile)
		{
			var existingMovie = _movies.FirstOrDefault(m => m.Id == id);
			
			if(existingMovie == null)
			{
				return null; // Фильм не найден
			}

			// Обновляем основные свойства
			existingMovie.Title = updatedMovie.Title;
			existingMovie.Genre = updatedMovie.Genre;


			// Обработка нового изображения
			if (imageFile != null && imageFile.Length > 0)
			{
				// Удаляем старое изображение, если оно было
				if(string.IsNullOrEmpty(existingMovie.ImageUrl))
				{
					DeleteImage(existingMovie.ImageUrl);
				}

				// Сохраняем новое изображение и обновляем URL
				existingMovie.ImageUrl = await SaveImageAsync(imageFile);
			}

			// Если новое изображение не загружено, и старое было, оно остается.
			// Если старое было null, и новое не загружено, оно остается null.
			return existingMovie;
		}

		public Task<bool> DeleteMovieAsync(int id)
		{
			var movie = _movies.FirstOrDefault(m => m.Id == id);

			if(movie == null)
			{
				return Task.FromResult(false);// Фильм не найден
			}


			// Удаляем изображение, если оно было
			if(!string.IsNullOrEmpty(movie.ImageUrl))
			{
				DeleteImage(movie.ImageUrl);
			}

			_movies.Remove(movie);
			return Task.FromResult(true);
		}


		// --- Вспомогательные методы для работы с изображениями ---
		private async Task<string> SaveImageAsync(IFormFile file)
		{
			if(file == null || file.Length == 0)
			{
				return string.Empty;
			}

			// Создаем уникальное имя файла, чтобы избежать коллизий
			var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var filePath = Path.Combine(_imageUploadPath, uniqueFileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			// Возвращаем относительный путь, который Blazor может использовать.
			// Важно: Если Blazor app и API на разных доменах/портах,
			// нужно будет возвращать полный URL (например, "https://yourcdn.com/images/ﬁlename.jpg"
			// или "http://localhost:5000/Uploads/MovieImages/ﬁlename.jpg")
			// Для простоты, возвращаем путь, который предполагается доступным по HTTP.
			// Вам нужно будет настроить статические файлы в вашем API.
			return $"/Uploads/MovieImages/{uniqueFileName}"; // Пример относительного URL
		}

		private void DeleteImage(string imageUrl)
		{
			if (string.IsNullOrEmpty(imageUrl)) return;

			try
			{

				// Извлекаем имя файла из URL
				var fileName = Path.GetFileName(imageUrl);
				var filePath = Path.Combine(_imageUploadPath, fileName);

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
			}

			catch (Exception ex)
			{
				// Логируем ошибку удаления, но не прерываем операцию
				Console.WriteLine($"Error deleting image {imageUrl}:{ex.Message}");
			}
		}
	}
}
