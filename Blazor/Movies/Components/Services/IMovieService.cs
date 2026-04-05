using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;// Для IFormFile

namespace Movies.Components.Services
{
	public interface IMovieService
	{
		Task<IEnumerable<Movie>> GetAllMoviesAsync();
		Task<Movie> GetMovieByIdAsync();
		Task<Movie> AddMovieAsync(Movie movie, IFormFile?imageFile);
		Task<Movie> UpdateMovieAsync(int id, Movie movie, IFormFile? imageFile);
		Task<bool> DeleteMovieAsync(); 
	}
}
