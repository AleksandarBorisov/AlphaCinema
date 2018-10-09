using AlphaCinemaData.Context;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class MovieServices : IMovieServices
	{
		private readonly AlphaCinemaContext context;

		public MovieServices(AlphaCinemaContext context)
		{
			this.context = context;
		}

		public List<Guid> GetIDs()
		{
			var cityIDs = context
				.Movies
				.Select(m => m.Id)
				.ToList();

			return cityIDs;
		}

		public List<string> GetMovieNames(List<Guid> MovieIDs)
		{
			var movieNames = new List<string>();

			MovieIDs.ForEach(id =>
			{
				var result = context.Movies
				.Where(m => m.Id == id)
				.Select(m => m.Name)
				.ToList();

				movieNames.Add(result[0]);
			});

			return movieNames;
		}
	}
}
