using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class AddProjection : DisplayBaseCommand
    {
        private IProjectionsServices projectionsServices;
        private IMovieServices movieServices;
        private ICityServices cityServices;
        private IOpenHourServices openHourServices;

        public AddProjection(ICommandProcessor commandProcessor,IItemSelector selector,
            IProjectionsServices projectionsServices, IMovieServices movieServices,
            ICityServices cityServices, IOpenHourServices openHourServices)
            :base(commandProcessor,selector)
        {
            this.projectionsServices = projectionsServices;
            this.movieServices = movieServices;
            this.cityServices = cityServices;
            this.openHourServices = openHourServices;
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

            string enterProjection = "Format: MovieName(50) | CityName(50) | OpenHour(Format: HH:MMh)";

            selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
            selector.PrintAtPosition(enterProjection, startingRow * 1 + offSetFromTop, false);

            string projection = selector.ReadAtPosition(startingRow * 2 + offSetFromTop,
                enterProjection, false, 250);

            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());

            string[] projectionDetails = projection.Split('|');

            try
            {
                if(projectionDetails.Length != 3)
                {
                    throw new ArgumentException("Please enter valid count of projection attributes");
                }

                string movieName = projectionDetails[0].Trim();
                string cityName = projectionDetails[1].Trim();
                string openHour = projectionDetails[2].Trim();
                DateTime date = DateTime.Now;

                //Just clear the input from the console
                selector.PrintAtPosition(new string(' ', enterProjection.Length), 
                    startingRow * 1 + offSetFromTop, false);
                
                //Find cityId by cityName
                int cityID = this.cityServices.GetID(cityName);
                
                //Find movieId by movieName
                int movieID = this.movieServices.GetID(movieName);
                
                //Find openHourId by openHour
                int openHourID = this.openHourServices.GetID(openHour);

                this.projectionsServices.AddNewProjection(movieID, cityID, openHourID, date);

                string successfullyAdded = $"Projection {projectionDetails[0]} " +
                    $"sucessfully added to the database";

                selector.PrintAtPosition(successfullyAdded, startingRow * 1 + offSetFromTop, false);
                Thread.Sleep(2000);
                selector.PrintAtPosition(new string(' ', successfullyAdded.Length), startingRow * 1 + offSetFromTop, false);

                parameters[0] = "AdminMenu";

                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is EntityDoesntExistException)
                {
                    string wrongParametersDetals = ex.Message;

                    selector.PrintAtPosition(new string(' ', enterProjection.Length), startingRow * 1 + offSetFromTop, false);
                    selector.PrintAtPosition(wrongParametersDetals, startingRow * 4 + offSetFromTop, false);

                    string selected = selector.DisplayItems(displayItems);


                    selector.PrintAtPosition(new string(' ', wrongParametersDetals.Length), startingRow * 4 + offSetFromTop, false);

                    if (selected == "Retry")
                    {
                        commandProcessor.ExecuteCommand(parameters);
                    }
                    else if (selected == "Back")
                    {
                        commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
                    }
                    else if (selected == "Home")
                    {
                        commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
                    }
                }
               
            }
        }
    }
}
