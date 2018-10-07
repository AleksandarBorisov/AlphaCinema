using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaCinemaData.Models
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Projection> Projections { get; set; }
    }
}
