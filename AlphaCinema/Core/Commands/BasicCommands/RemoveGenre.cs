using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class RemoveGenre : ICommand
	{
		private readonly ICommandProcessor commandProcessor;
		private readonly IGenreServices genreServices;
		private readonly IAlphaCinemaConsole cinemaConsole;

		public RemoveGenre(ICommandProcessor commandProcessor, IGenreServices genreServices, IAlphaCinemaConsole cinemaConsole)
		{
			this.commandProcessor = commandProcessor;
			this.genreServices = genreServices;
			this.cinemaConsole = cinemaConsole;
		}

		public void Execute(List<string> parameters)
		{
			// Ако някой измисли нещо по-добро да го оправи, засега това се сещам набързо
			cinemaConsole.Clear();
			cinemaConsole.WriteLine("Type a genre:\n");
			var genreName = cinemaConsole.ReadLine().Trim();

			try
			{
				genreServices.DeleteGenre(genreName);
			}
			catch (EntityDoesntExistException e)
			{
				cinemaConsole.WriteLine("\n" + e.Message);
				cinemaConsole.Write("\nPress any key to go back...");
				cinemaConsole.ReadKey();
				cinemaConsole.Clear();

				commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());

			}
			cinemaConsole.WriteLine("\nSuccessfully deleted from database");
			cinemaConsole.Write("\nPress any key to go back...");
			cinemaConsole.ReadKey();
			cinemaConsole.Clear();

			commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
		}
	}
}