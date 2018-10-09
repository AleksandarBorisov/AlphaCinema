using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public ICollection<MovieGenre> MoviesGenres { get; set; }
    }
}
