using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class AddGenre : ICommand
	{
		private readonly ICommandProcessor commandProcessor;
		private readonly IGenreServices genreServices;
		private readonly IAlphaCinemaConsole cinemaConsole;

		public AddGenre(ICommandProcessor commandProcessor, IGenreServices genreServices, IAlphaCinemaConsole cinemaConsole)
		{
			this.commandProcessor = commandProcessor;
			this.genreServices = genreServices;
			this.cinemaConsole = cinemaConsole;
		}

		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Type a genre name:\n");

			try
			{
				var genreName = cinemaConsole.ReadLineMiddle().TrimEnd().TrimStart();
				Validations(genreName);

                genreServices.AddNewGenre(genreName);
				cinemaConsole.HandleOperation("\nSuccessfully added to database");
			}
			catch (InvalidClientInputException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			catch (EntityAlreadyExistsException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			finally
			{
				commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
			}
		}

		private void Validations(string genreName)
		{
			if (string.IsNullOrWhiteSpace(genreName))
			{
				throw new InvalidClientInputException("\nInvalid genre name");
			}
			if (genreName.All(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nGenre cannot be only digits");
			}
		}
	}
}
