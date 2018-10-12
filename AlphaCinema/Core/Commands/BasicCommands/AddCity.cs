using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	class AddCity : ICommand
	{
		private readonly ICommandProcessor commandProcessor;
		private readonly ICityServices cityServices;
		private readonly IAlphaCinemaConsole cinemaConsole;

		public AddCity(ICommandProcessor commandProcessor, ICityServices cityServices, IAlphaCinemaConsole cinemaConsole)
		{
			this.commandProcessor = commandProcessor;
			this.cityServices = cityServices;
			this.cinemaConsole = cinemaConsole;
		}

		public void Execute(List<string> parameters)
		{
			cinemaConsole.Clear();
			Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
			cinemaConsole.WriteLine("Type a city:\n");
			
			try
			{
				Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 2);
				var cityName = Console.ReadLine().Trim();
				//var cityName = cinemaConsole.ReadLine().Trim();
				Validations(cityName);
				cityServices.AddNewCity(cityName);
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
