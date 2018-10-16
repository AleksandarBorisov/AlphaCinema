using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class RemoveProjection : ICommand
    {
        private readonly IAlphaCinemaConsole cinemaConsole;
        private readonly IMovieServices movieServices;
        private readonly ICityServices cityServices;
        private readonly IOpenHourServices openHourServices;
        private readonly IProjectionsServices projectionsServices;

        public RemoveProjection(IAlphaCinemaConsole cinemaConsole,
            IMovieServices movieServices, ICityServices cityServices,
            IOpenHourServices openHourServices, IProjectionsServices projectionsServices)
        {
            this.cityServices = cityServices;
            this.openHourServices = openHourServices;
            this.projectionsServices = projectionsServices;
            this.movieServices = movieServices;
            this.cinemaConsole = cinemaConsole;
        }
        public IEnumerable<string> Execute(IEnumerable<string> inputAsIEnumerable)
        {
            var parameters = inputAsIEnumerable.ToList();
            cinemaConsole.Clear();
            cinemaConsole.WriteLineMiddle("Format: MovieName(50) | CityName(50) | OpenHour (Format: HH:MMh) | Date (Format: YYYY-MM-DD)", 0);

            try
            {
                var input = cinemaConsole.ReadLineMiddle(0).Split('|');
                Validations(input);

                var movieID = movieServices.GetID(input[0].TrimEnd().TrimStart());
                var cityID = cityServices.GetID(input[1].TrimEnd().TrimStart());
                var openHourID = openHourServices.GetID(input[2].TrimEnd().TrimStart());


                projectionsServices.Delete(cityID, movieID, openHourID);
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
            if (input.Length != 3)
            {
                throw new InvalidClientInputException("Invalid parameters input");
            }
            var movieName = input[0];
            var genreName = input[1];
			var openHour = input[2];

            if (string.IsNullOrWhiteSpace(movieName) || string.IsNullOrWhiteSpace(genreName)
				|| string.IsNullOrWhiteSpace(openHour))
            {
                throw new InvalidClientInputException("\nInvalid input");
            }
            if ((genreName.Any(c => char.IsDigit(c))))
            {
                throw new InvalidClientInputException("\nInput cannot be only digits");
            }
        }
    }
}
