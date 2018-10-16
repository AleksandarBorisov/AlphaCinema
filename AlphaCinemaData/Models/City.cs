using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class City : Entity
    {
		virtual public string Name { get; set; }
		virtual public ICollection<Projection> Projections { get; set; }

    }
}
