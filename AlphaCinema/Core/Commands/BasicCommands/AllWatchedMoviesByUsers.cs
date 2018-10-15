using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class AllWatchedMoviesByUsers : ICommand
	{
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IWatchedMovieServices watchedMovieServices;
		private readonly IUserServices userServices;
		private readonly IPdfExporter pdfExporter;
		private readonly SortedDictionary<string, HashSet<Movie>> watchedMovies = new SortedDictionary<string, HashSet<Movie>>();

		public AllWatchedMoviesByUsers(ICommandProcessor commandProcessor, IAlphaCinemaConsole cinemaConsole,
			IWatchedMovieServices watchedMovieServices, IUserServices userServices, IPdfExporter pdfExporter)
		{
			this.cinemaConsole = cinemaConsole;
			this.watchedMovieServices = watchedMovieServices;
			this.userServices = userServices;
			this.pdfExporter = pdfExporter;
		}

		public IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            cinemaConsole.Clear();
			try
			{
				FillCollectionWithData();
				pdfExporter.ExportWatchedMoviesByUsers(watchedMovies);
				cinemaConsole.HandleOperation("\nSuccessfully exported data to PDF");
                return parameters.Skip(1);
            }
			catch (EmptyEntityTableException e)
			{
				cinemaConsole.HandleException(e.Message);
                return parameters.Skip(1);
            }
			catch (iText.Kernel.PdfException e)
			{
				cinemaConsole.HandleException(e.Message);
                return parameters.Skip(1);
            }
		}

		private void FillCollectionWithData()
		{
			var users = userServices.GetUsers();
			Validations(users);

			foreach (var user in users)
			{
				var moviesByUser = watchedMovieServices.GetWatchedMoviesByUserName(user.Name, user.Age);
				foreach (var movie in moviesByUser)
				{
					if (!watchedMovies.ContainsKey(user.Name))
					{
						watchedMovies[user.Name] = new HashSet<Movie>();
					}
					watchedMovies[user.Name].Add(movie);
				}
			}
		}

		private void Validations(HashSet<User> users)
		{
			if (users.Count == 0)
			{
				throw new EmptyEntityTableException("\nNo users found in database");
			}
		}
	}
}
