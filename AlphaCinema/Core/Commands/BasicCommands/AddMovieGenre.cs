using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class AddMovieGenre : ICommand
	{
		private readonly ICommandProcessor commandProcessor;
		private readonly IGenreServices genreServices;
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IMovieServices movieServices;
		private readonly IMovieGenreServices movieGenreServices;

		public AddMovieGenre(ICommandProcessor commandProcessor, IGenreServices genreServices,
			IAlphaCinemaConsole cinemaConsole, IMovieServices movieServices, IMovieGenreServices movieGenreServices)
		{
			this.commandProcessor = commandProcessor;
			this.genreServices = genreServices;
			this.cinemaConsole = cinemaConsole;
			this.movieServices = movieServices;
			this.movieGenreServices = movieGenreServices;
		}

		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			cinemaConsole.WriteLine("Type a movie name and a genre like this:");
			cinemaConsole.WriteLine("Movie Name | Genre");
			try
			{
				var input = cinemaConsole.ReadLine().Split('|');
				Validations(input);

				var movieID = movieServices.GetID(input[0].TrimEnd().TrimStart());
				var genreID = genreServices.GetID(input[1].TrimEnd().TrimStart());

				movieGenreServices.AddNew(movieID, genreID);
				cinemaConsole.HandleOperation("\nSuccessfully added to database");
			}
			catch (InvalidClientInputException e)
			{
				cinemaConsole.HandleException(e.Message);
			}
			catch (EntityDoesntExistException e)
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
			if ((movieName.All(c => char.IsDigit(c))) || (genreName.All(c => char.IsDigit(c))))
			{
				throw new InvalidClientInputException("\nInput cannot be only digits");
			}
		}
	}
}
