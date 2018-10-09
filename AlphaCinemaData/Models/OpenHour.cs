using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaCinemaData.Models
{
    public class OpenHour : Entity
    {
        public string StartHour { get; set; }
        public ICollection<Projection> Projections { get; set; }

    }
}
