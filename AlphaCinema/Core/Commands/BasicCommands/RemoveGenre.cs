using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System;

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
			cinemaConsole.Clear();
			cinemaConsole.WriteLine("Type a city:\n");

			try
			{
			  	var genreName = cinemaConsole.ReadLine().Trim();
				Validations(genreName);
				genreServices.DeleteGenre(genreName);
				cinemaConsole.HandleOperation("\nSuccessfully deleted from database");
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

		private void Validations(string genreName)
		{
			if (string.IsNullOrWhiteSpace(genreName))
			{
				throw new InvalidClientInputException("\nInvalid genre name");
			}
			if (genreName.Any(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nGenre cannot be only digits");
			}
		}
	}
}