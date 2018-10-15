using System.Collections.Generic;
using System.Linq;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class RemoveMovieGenre : ICommand
	{
		private readonly IGenreServices genreServices;
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IMovieServices movieServices;
		private readonly IMovieGenreServices movieGenreServices;

		public RemoveMovieGenre(IGenreServices genreServices,
			IAlphaCinemaConsole cinemaConsole, IMovieServices movieServices, IMovieGenreServices movieGenreServices)
		{
			this.genreServices = genreServices;
			this.cinemaConsole = cinemaConsole;
			this.movieServices = movieServices;
			this.movieGenreServices = movieGenreServices;
		}
		public IEnumerable<string> Execute(IEnumerable<string> inputAsIEnumerable)
		{
            var parameters = inputAsIEnumerable.ToList();
            cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Format: Movie Name | Genre");

            try
			{
				var input = cinemaConsole.ReadLineMiddle().Split('|');
				Validations(input);

				var movieID = movieServices.GetID(input[0].TrimEnd().TrimStart());
				var genreID = genreServices.GetID(input[1].TrimEnd().TrimStart());

				movieGenreServices.Delete(movieID, genreID);
				cinemaConsole.HandleOperation("\nSuccessfully deleted from database");
                return parameters.Skip(1);
            }
			catch (InvalidClientInputException e)
			{
				cinemaConsole.HandleException(e.Message);
                return parameters.Skip(1);
            }
			catch (EntityDoesntExistException e)
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
		private void Validations(string[] input)
		{
			if (input.Length != 2)
			{
				throw new InvalidClientInputException("Invalid parameters input");
			}
			var movieName = input[0];
			var genreName = input[1];

			if (string.IsNullOrWhiteSpace(movieName) || string.IsNullOrWhiteSpace(genreName))
			{
				throw new InvalidClientInputException("\nInvalid name");
			}
			if (genreName.Any(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nGenre name cannot be only digits");
			}
		}
	}
}
