﻿using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class WatchedMoviesByUser : ICommand
	{
		private readonly IAlphaCinemaConsole cinemaConsole;
		private readonly IWatchedMovieServices watchedMovieServices;
		private readonly IUserServices userServices;
		private readonly IPdfExporter pdfExporter;

		public WatchedMoviesByUser(IAlphaCinemaConsole cinemaConsole,
			IWatchedMovieServices watchedMovieServices, IUserServices userServices, IPdfExporter pdfExporter)
		{
            this.cinemaConsole = cinemaConsole;
			this.pdfExporter = pdfExporter;
            this.watchedMovieServices = watchedMovieServices;
			this.userServices = userServices;
		}
		public IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            cinemaConsole.Clear();
			cinemaConsole.WriteLineMiddle("Type a user name | age:\n");

            try
			{
				var clientInput = cinemaConsole.ReadLineMiddle().Split('|');
				var userName = clientInput[0];
				if (!int.TryParse(clientInput[1], out int userAge))
				{
					throw new InvalidClientInputException("Age must be integer number");
				}
				Validations(userName);

                var movies = watchedMovieServices.GetWatchedMoviesByUserName(userName, userAge);

                pdfExporter.ExportUserWatchedMovies(movies, userName);
				cinemaConsole.HandleOperation("\nSuccessfully exported data to PDF");
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

		private void Validations(string userName)
		{
			var user = userServices.GetUser(userName);
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new InvalidClientInputException("\nInvalid user name");
			}
			if (user == null || user.IsDeleted)
			{
				throw new EntityDoesntExistException("\nUser is not present in the database.");
			}
			if (userName.All(c => char.IsDigit(c)))
			{
				throw new InvalidClientInputException("\nUser cannot be only digits");
			}
		}
	}
}
