using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class User : Entity
    {
		virtual public string Name { get; set; }
		virtual public int Age { get; set; }
		virtual public ICollection<WatchedMovie> WatchedMovies { get; set; }
    }
}
