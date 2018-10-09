using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaCinemaData.Models
{
    public class City : Entity
    {
        public string Name { get; set; }
        public ICollection<Projection> Projections { get; set; }

    }
}
