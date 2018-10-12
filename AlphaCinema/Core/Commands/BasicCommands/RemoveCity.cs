using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    class RemoveCity : ICommand
    {
		private readonly ICommandProcessor commandProcessor;
		private readonly ICityServices cityServices;
		private readonly IAlphaCinemaConsole cinemaConsole;

		public RemoveCity(ICommandProcessor commandProcessor, ICityServices cityServices, IAlphaCinemaConsole cinemaConsole)
		{
			this.commandProcessor = commandProcessor;
			this.cityServices = cityServices;
			this.cinemaConsole = cinemaConsole;
		}

		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			cinemaConsole.WriteLine("Type a city:\n");
			try
			{
				var cityName = cinemaConsole.ReadLine().Trim();

                Validations(cityName);

                cityServices.DeleteCity(cityName);
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

		private void Validations(string cityName)
		{
			if (string.IsNullOrWhiteSpace(cityName))
			{
				throw new InvalidClientInputException("\nInvalid city name");
			}
			if (cityName.All(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nCity cannot be only digits");
			}
		}
	}
}
