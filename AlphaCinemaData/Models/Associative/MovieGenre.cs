using AlphaCinemaData.Models.Contracts;
using System;

namespace AlphaCinemaData.Models.Associative
{
    public class MovieGenre : IDeletable
    {
        public Guid MovieId { get; set; }
        
        public Guid GenreId { get; set; }

        public Movie Movie { get; set; }

        public Genre Genre { get; set; }

		public bool IsDeleted { get; set; }

	}
}
