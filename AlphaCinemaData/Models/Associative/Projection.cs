using AlphaCinemaData.Models.Contracts;
using System;
using System.Collections.Generic;

namespace AlphaCinemaData.Models.Associative
{
    public class Projection : IDeletable
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }

        public Guid CityId { get; set; }

        public Guid OpenHourId { get; set; }

        public DateTime Date { get; set; }

        public Movie Movie { get; set; }

        public City City { get; set; }

        public OpenHour OpenHour { get; set; }

        public ICollection<WatchedMovie> WatchedMovies { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
