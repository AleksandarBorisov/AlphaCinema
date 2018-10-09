using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class City : Entity
    {
        public string Name { get; set; }
        public ICollection<Projection> Projections { get; set; }

    }
}
