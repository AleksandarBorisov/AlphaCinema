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
		private readonly ICommandProcessor commandProcessor;
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IWatchedMovieServices watchedMovieServices;
		private readonly IUserServices userServices;
		private readonly IPdfExporter pdfExporter;
		private readonly SortedDictionary<string, HashSet<Movie>> watchedMovies = new SortedDictionary<string, HashSet<Movie>>();

		public AllWatchedMoviesByUsers(ICommandProcessor commandProcessor, IAlphaCinemaConsole cinemaConsole,
			IWatchedMovieServices watchedMovieServices, IUserServices userServices, IPdfExporter pdfExporter)
		{
			this.commandProcessor = commandProcessor;
			this.cinemaConsole = cinemaConsole;
			this.watchedMovieServices = watchedMovieServices;
			this.userServices = userServices;
			this.pdfExporter = pdfExporter;
		}

		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			try
			{
				FillCollectionWithData();
				pdfExporter.ExportWatchedMoviesByUsers(watchedMovies);
				cinemaConsole.HandleOperation("\nSuccessfully exported data to PDF");
			}
			catch (EmptyEntityTableException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			catch (iText.Kernel.PdfException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			finally
			{
				commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
			}
		}

		private void FillCollectionWithData()
		{
			var users = userServices.GetUsers();
			Validations(users);

			foreach (var user in users)
			{
				var moviesByUser = watchedMovieServices.GetWatchedMoviesByUserName(user.Name);
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
