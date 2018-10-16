using AlphaCinemaData.Models.Contracts;
using System;
using System.Collections.Generic;

namespace AlphaCinemaData.Models.Associative
{
    public class Projection : IDeletable
    {
		virtual public int Id { get; set; }

		virtual public int MovieId { get; set; }

		virtual public int CityId { get; set; }

		virtual public int OpenHourId { get; set; }


		virtual public Movie Movie { get; set; }

		virtual public City City { get; set; }

		virtual public OpenHour OpenHour { get; set; }	
		virtual public ICollection<WatchedMovie> WatchedMovies { get; set; }

		virtual public bool IsDeleted { get; set; }
	}
}
