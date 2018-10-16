using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class Genre : Entity
    {
		virtual public string Name { get; set; }
		virtual public ICollection<MovieGenre> MoviesGenres { get; set; }
    }
}
