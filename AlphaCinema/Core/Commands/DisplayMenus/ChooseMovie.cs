using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class ChooseMovie : DisplayBaseCommand
    {
		private readonly IMovieServices movieServices;
        
		public ChooseMovie(ICommandProcessor commandProcessor, IItemSelector selector, 
            IMovieServices movieServices)
			: base(commandProcessor, selector)
        {
			this.movieServices = movieServices;
        }

		public override void Execute(List<string> parameters)
		{
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];

			var movieNames = this.movieServices.GetMovieNames();
            List<string> displayItems = new List<string>() { "Choose Movie"};

            displayItems.AddRange(movieNames);
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);
            string movieName = selector.DisplayItems(displayItems);
            if (movieName == "Back")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
            }
            else if (movieName == "Home")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(5).ToList());
            }
            else
            {
				var movieID = this.movieServices.GetID(movieName);
                parameters.Insert(0, movieID);
                parameters.Insert(0, "ChooseHour");//Тук се налага да напишем командата ръчно
                commandProcessor.ExecuteCommand(parameters);
            }

        }
    }
}
