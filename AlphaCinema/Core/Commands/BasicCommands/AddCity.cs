using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class AddCity : ICommand
	{
		private readonly ICityServices cityServices;
		private readonly IAlphaCinemaConsole cinemaConsole;

		public AddCity(ICityServices cityServices, IAlphaCinemaConsole cinemaConsole)
		{
			this.cityServices = cityServices;
			this.cinemaConsole = cinemaConsole;
		}

		public IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Type a city name:\n");
			try
			{
				var cityName = cinemaConsole.ReadLineMiddle().TrimEnd().TrimStart();
				Validations(cityName);
				cityServices.AddNewCity(cityName);
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

		private void Validations(string cityName)
		{
			if (string.IsNullOrWhiteSpace(cityName))
			{
				throw new InvalidClientInputException("\nInvalid city name");
			}
			if (cityName.Any(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nCity cannot be only digits");
			}
		}
	}
}
