using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaCinemaData.Models.Associative
{
    public class MovieGenre
    {
       // [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        
       // [ForeignKey("Genre")]
        public Guid GenreId { get; set; }

        public Movie Movie { get; set; }

        public Genre Genre { get; set; }
    }
}
