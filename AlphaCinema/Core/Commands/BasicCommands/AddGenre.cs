using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class AddGenre : ICommand
	{
		private readonly IGenreServices genreServices;
		private readonly IAlphaCinemaConsole cinemaConsole;

		public AddGenre(IGenreServices genreServices, IAlphaCinemaConsole cinemaConsole)
		{
			this.genreServices = genreServices;
			this.cinemaConsole = cinemaConsole;
		}

		public IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Type a genre name:\n");

			try
			{
				var genreName = cinemaConsole.ReadLineMiddle().TrimEnd().TrimStart();
				Validations(genreName);

                genreServices.AddNewGenre(genreName);
				cinemaConsole.HandleOperation("\nSuccessfully added to database");
                return parameters.Skip(1);
            }
			catch (InvalidClientInputException e)
			{
				cinemaConsole.HandleException(e.Message);
                return parameters.Skip(1);
            }
			catch (EntityAlreadyExistsException e)
			{
				cinemaConsole.HandleException(e.Message);
                return parameters.Skip(1);
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
