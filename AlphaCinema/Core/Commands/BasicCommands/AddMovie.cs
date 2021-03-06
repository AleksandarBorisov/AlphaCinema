﻿using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class AddMovie : DisplayBaseCommand
    {
        private IMovieServices movieServices;

        public AddMovie(IItemSelector selector, IMovieServices movieServices)
            : base(selector)
        {
            this.movieServices = movieServices;
        }

        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            var parameters = input.ToList();
            int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
            int startingRow = int.Parse(parameters[parameters.Count - 1]);

            List<string> displayItems = new List<string>
            {
                parameters[0],
                "Retry",
                "Back",
                "Home"
            };

            string enterМovie = "Format: MovieName(50) | Description(150) |  RealeaseYear | Duration";

            selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
            selector.PrintAtPosition(enterМovie, startingRow * 1 + offSetFromTop, false);

            string movie = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, enterМovie, false, 250);

            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());

            string[] movieDetails = movie.Split('|');

            try
            {
                if (movieDetails.Length != 4)
                {
                    throw new ArgumentException("Please enter valid count of movie attributes");
                }

                string name = movieDetails[0];
                string description = movieDetails[1];

                if (!int.TryParse(movieDetails[2].Trim(), out int releaseYear))
                {
                    throw new ArgumentException("Movie ReleaseYear must be integer number");
                }
                if (!int.TryParse(movieDetails[3].Trim(), out int duration))
                {
                    throw new ArgumentException("Movie Duration must be integer number");
                }

                selector.PrintAtPosition(new string(' ', enterМovie.Length), startingRow * 1 + offSetFromTop, false);

                movieServices.AddNewMovie(name, description, releaseYear, duration);

                string successfullyAdded = $"Movie {movieDetails[0].Trim()} sucessfully added to the database";

                selector.PrintAtPosition(successfullyAdded, startingRow * 1 + offSetFromTop, false);
                Thread.Sleep(2000);
                selector.PrintAtPosition(new string(' ', successfullyAdded.Length), startingRow * 1 + offSetFromTop, false);

                parameters[0] = "AdminMenu";

                return parameters.Skip(1);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is EntityAlreadyExistsException || ex is NullReferenceException)
                {
                    string wrongParametersDetails = ex.Message;

                    selector.PrintAtPosition(new string(' ', enterМovie.Length), startingRow * 1 + offSetFromTop, false);
                    selector.PrintAtPosition(wrongParametersDetails, startingRow * 4 + offSetFromTop, false);

                    string selected = selector.DisplayItems(displayItems);

                    selector.PrintAtPosition(new string(' ', wrongParametersDetails.Length), startingRow * 4 + offSetFromTop, false);

                    if (selected == "Retry")
                    {
                        return parameters;
                    }
                    else if (selected == "Back")
                    {
                        return parameters.Skip(1);
                    }
                    else if (selected == "Home")
                    {
                        return parameters.Skip(2);
                    }
                }
                return ex.Message.ToString().Split();
            }
        }
    }
}
