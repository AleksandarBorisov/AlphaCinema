using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class AddMovie : DisplayBaseCommand
    {
        private IMovieServices movieServices;

        public AddMovie(ICommandProcessor commandProcessor, IItemSelector selector, IMovieServices movieServices)
            : base(commandProcessor, selector)
        {
            this.movieServices = movieServices;
        }

        public override void Execute(List<string> parameters)
        {
            int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
            int startingRow = int.Parse(parameters[parameters.Count - 1]);
            List<string> displayItems = new List<string>
            {
                parameters[0],
                "Retry",
                "Back",
                "Home"
            };
            string enterМovie = "Format: MovieName(50), Description(150), RealeaseYear, Duration";
            selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
            selector.PrintAtPosition(enterМovie, startingRow * 1 + offSetFromTop, false);
            string movie = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, enterМovie);
            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());
            string[] movieDetals = movie.Split(',');
            try
            {
                selector.PrintAtPosition(new string(' ', enterМovie.Length), startingRow * 1 + offSetFromTop, false);
                if (movieDetals.Length != 4)
                {
                    throw new ArgumentException("Please enter valid count of movie attributes");
                }
                //movieServices.AddNewMovie(movieDetals);
                string successfullyAdded = $"Movie {movieDetals[0]} sucessfully added to the datavase";
                selector.PrintAtPosition(successfullyAdded, startingRow * 1 + offSetFromTop, false);
                Thread.Sleep(300);
                parameters[0] = "AdminMenu";
                commandProcessor.ExecuteCommand(parameters);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    string wrongParametersDetals = ex.Message;
                    selector.PrintAtPosition(wrongParametersDetals, startingRow * 4 + offSetFromTop, false);
                    string selected = selector.DisplayItems(displayItems);
                    if (selected == "Retry")
                    {
                        selector.PrintAtPosition(new string(' ', wrongParametersDetals.Length), startingRow * 4 + offSetFromTop, false);
                        commandProcessor.ExecuteCommand(parameters);
                    }
                    else if (selected == "Back")
                    {

                    }
                    else if (selected == "Home")
                    {

                    }
                }
                
            }
        }
    }
}
