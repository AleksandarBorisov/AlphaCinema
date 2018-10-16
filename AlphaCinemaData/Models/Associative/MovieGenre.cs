using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Models.Associative
{
    public class MovieGenre : IDeletable
    {
		virtual public int MovieId { get; set; }

		virtual public int GenreId { get; set; }

		virtual public Movie Movie { get; set; }

		virtual public Genre Genre { get; set; }

		virtual public bool IsDeleted { get; set; }

	}
}
