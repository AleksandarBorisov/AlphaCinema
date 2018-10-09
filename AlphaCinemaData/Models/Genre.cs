using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaCinemaData.Models
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public ICollection<MovieGenre> MoviesGenres { get; set; }
    }
}
