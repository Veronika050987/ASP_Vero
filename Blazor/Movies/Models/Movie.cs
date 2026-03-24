using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 4)]
		public string Title { get; set; }

		[RangeAttribute(typeof(DateOnly), "1888-10-14", "9999-12-31")]
		public DateOnly ReleaseDate { get; set; }

		public string Genre { get; set; }
		public string? URL { get; set; }

		public string? Brief { get; set; }

		//public string? Poster { get; set; }

		public byte[]? PosterImage { get; set; }

		//Добавлено для хранения MIME-типа изображения (например, image/jpeg, image/png).
		[StringLength(50)]
		public string? PosterMimeType { get; set; }
	}
}
