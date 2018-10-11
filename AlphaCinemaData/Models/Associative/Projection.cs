using AlphaCinemaData.Models.Contracts;
using System;
using System.Collections.Generic;

namespace AlphaCinemaData.Models.Associative
{
    public class Projection : IDeletable
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int CityId { get; set; }

        public int OpenHourId { get; set; }

        public DateTime Date { get; set; }

        public Movie Movie { get; set; }

        public City City { get; set; }

        public OpenHour OpenHour { get; set; }

        public ICollection<WatchedMovie> WatchedMovies { get; set; }

		public bool IsDeleted { get; set; }
	}
}
