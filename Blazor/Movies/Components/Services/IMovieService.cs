using Movies.Models;
using Movies.Components.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;// Для IFormFile

namespace Movies.Components.Services
{
	public interface IMovieService
	{
		Task<IEnumerable<Movie>> GetAllMoviesAsync();
		Task<Movie?> GetMovieByIdAsync(int id);
		Task<Movie> AddMovieAsync(Movie movie, IFormFile?imageFile);
		Task<Movie> UpdateMovieAsync(int id, Movie movie, IFormFile? imageFile);
		Task<bool> DeleteMovieAsync(int id); 
	}
}
