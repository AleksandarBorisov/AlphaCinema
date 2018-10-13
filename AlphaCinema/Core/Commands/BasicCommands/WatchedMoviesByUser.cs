using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class WatchedMoviesByUser : ICommand
	{
		private readonly ICommandProcessor commandProcessor;
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IWatchedMovieServices watchedMovieServices;
		private readonly IUserServices userServices;
		private readonly IPdfExporter pdfExporter;

		public WatchedMoviesByUser(ICommandProcessor commandProcessor, IAlphaCinemaConsole cinemaConsole,
			IWatchedMovieServices watchedMovieServices, IUserServices userServices, IPdfExporter pdfExporter)
		{
			this.commandProcessor = commandProcessor;
            this.cinemaConsole = cinemaConsole;
			this.pdfExporter = pdfExporter;

            this.watchedMovieServices = watchedMovieServices;
			this.userServices = userServices;
		}
		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Type a user name:\n");

            try
			{
				var userName = cinemaConsole.ReadLineMiddle().TrimEnd().TrimStart();
				Validations(userName);

                var movies = watchedMovieServices.GetWatchedMoviesByUserName(userName);

                pdfExporter.ExportUserWatchedMovies(movies, userName);
				cinemaConsole.HandleOperation("\nSuccessfully exported data to PDF");
			}
			catch (InvalidClientInputException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			catch (EntityDoesntExistException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			finally
			{
				commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
			}
		}

		private void Validations(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new InvalidClientInputException("\nInvalid user name");
			}
			if (!userServices.IfExist(userName) || userServices.IsDeleted(userName))
			{
				throw new EntityDoesntExistException("\nUser is not present in the database.");
			}	
			if (userName.All(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nUser cannot be only digits");
			}
		}
	}
}
