using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class RemoveProjection : ICommand
	{
		private readonly ICommandProcessor commandProcessor;
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IMovieServices movieServices;
		private readonly ICityServices cityServices;
		private readonly IOpenHourServices openHourServices;
		private readonly IProjectionsServices projectionsServices;

		public RemoveProjection(ICommandProcessor commandProcessor, IAlphaCinemaConsole cinemaConsole,
			IMovieServices movieServices, ICityServices cityServices,
			IOpenHourServices openHourServices, IProjectionsServices projectionsServices)
		{
			this.commandProcessor = commandProcessor;
			this.cinemaConsole = cinemaConsole;
			this.movieServices = movieServices;
			this.cityServices = cityServices;
			this.openHourServices = openHourServices;
			this.projectionsServices = projectionsServices;
			this.cinemaConsole = cinemaConsole;
			this.movieServices = movieServices;
			this.commandProcessor = commandProcessor;
		}
		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			cinemaConsole.WriteLine("Type a projection:");
			cinemaConsole.WriteLine("Format: MovieName(50) | CityName(50) | OpenHour (Format: HH:MMh) | Date (Format: YYYY-MM-DD)");

			try
			{
				var input = cinemaConsole.ReadLine().Split('|');
				Validations(input);

				var movieID = movieServices.GetID(input[0].TrimEnd().TrimStart());
				var cityID = cityServices.GetID(input[1].TrimEnd().TrimStart());
				var openHourID = openHourServices.GetID(input[2].TrimEnd().TrimStart());
				var date = projectionsServices.GetDate(movieID, cityID, openHourID);

				projectionsServices.Delete(movieID, cityID, openHourID, date);
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
			if (input.Length != 4)
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
