using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;

namespace AlphaCinemaData.Models
{
    public class OpenHour : Entity
    {
		virtual public string StartHour { get; set; }
		virtual public ICollection<Projection> Projections { get; set; }

    }
}
