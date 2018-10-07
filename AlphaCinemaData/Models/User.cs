using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaCinemaData.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<WatchedMovie> WatchedMovies { get; set; }
    }
}
