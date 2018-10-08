using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaCinemaData.Models.Associative
{
    public class Projection
    {
      //  [Key]
        public Guid Id { get; set; }

      //  [ForeignKey("Movie")]
        public Guid MovieId { get; set; }

       // [ForeignKey("City")]
        public Guid CityId { get; set; }

      //  [ForeignKey("OpenHour")]
        public Guid OpenHourId { get; set; }

        public DateTime Date { get; set; }

        public Movie Movie { get; set; }

        public City City { get; set; }

        public OpenHour OpenHour { get; set; }

        public ICollection<WatchedMovie> WatchedMovies { get; set; }
    }
}
