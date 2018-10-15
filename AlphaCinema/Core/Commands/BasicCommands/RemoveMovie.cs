using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class RemoveMovie : ICommand
    {
        private readonly IMovieServices movieServices;
        private readonly IAlphaCinemaConsole cinemaConsole;

        public RemoveMovie(IMovieServices movieServices,
            IAlphaCinemaConsole cinemaConsole)
        {
            this.movieServices = movieServices;
            this.cinemaConsole = cinemaConsole;
        }

        public IEnumerable<string> Execute(IEnumerable<string> input)
        {
            var parameters = input.ToList();
            cinemaConsole.Clear();
            cinemaConsole.WriteLineMiddle("Type a movie name:\n");

            try
            {
                string movieName = cinemaConsole.ReadLineMiddle().TrimEnd().TrimStart();
                Validations(movieName);
                movieServices.DeleteMovie(movieName);
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
