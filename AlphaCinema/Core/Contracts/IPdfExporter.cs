using AlphaCinemaData.Models;
using System.Collections.Generic;

namespace AlphaCinema.Core.Utilities
{
	public interface IPdfExporter
	{
		void ExportUserWatchedMovies(IEnumerable<Movie> movies, string userName);
	}
}