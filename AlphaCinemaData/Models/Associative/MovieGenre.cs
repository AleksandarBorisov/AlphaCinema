using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Models.Associative
{
    public class MovieGenre : IDeletable
    {
		public int MovieId { get; set; }
        
        public int GenreId { get; set; }

        public Movie Movie { get; set; }

        public Genre Genre { get; set; }

		public bool IsDeleted { get; set; }

	}
}
