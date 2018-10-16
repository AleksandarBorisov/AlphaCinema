using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class Movie : Entity
    {
        virtual public string Name { get; set; }
		virtual public string Description { get; set; }
		virtual public int ReleaseYear { get; set; }
		virtual public int Duration { get; set; }
		virtual public ICollection<MovieGenre> MovieGenres { get; set; }
		virtual public ICollection<Projection> Projections { get; set; }
    }
}
