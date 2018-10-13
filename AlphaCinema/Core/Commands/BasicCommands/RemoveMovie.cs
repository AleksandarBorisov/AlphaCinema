using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class RemoveMovie : ICommand
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IMovieServices movieServices;
        private readonly IAlphaCinemaConsole cinemaConsole;

        public RemoveMovie(ICommandProcessor commandProcessor, IMovieServices movieServices,
            IAlphaCinemaConsole cinemaConsole)
        {
            this.commandProcessor = commandProcessor;
            this.movieServices = movieServices;
            this.cinemaConsole = cinemaConsole;
        }

        public void Execute(List<string> parameters)
        {
            cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Type a movie name:\n");

			try
            {
				string movieName = cinemaConsole.ReadLineMiddle().TrimEnd().TrimStart();
				Validations(movieName);

                movieServices.DeleteMovie(movieName);
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

        private void Validations(string movieName)
        {
            if (string.IsNullOrWhiteSpace(movieName))
            {
                throw new InvalidClientInputException("\nInvalid movie name");
            }
        }

    }
}
