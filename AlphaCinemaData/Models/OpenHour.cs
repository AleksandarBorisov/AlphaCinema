using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class OpenHour : Entity
    {
        public string StartHour { get; set; }
        public ICollection<Projection> Projections { get; set; }

    }
}
