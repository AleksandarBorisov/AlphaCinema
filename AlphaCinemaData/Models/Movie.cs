using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class Movie : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<Projection> Projections { get; set; }
    }
}
