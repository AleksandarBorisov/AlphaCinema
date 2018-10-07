using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaCinemaData.Models
{
    public class OpenHour
    {
        [Key]
        public Guid Id { get; set; }
        public string StartHour { get; set; }
        public ICollection<Projection> Projections { get; set; }

    }
}
