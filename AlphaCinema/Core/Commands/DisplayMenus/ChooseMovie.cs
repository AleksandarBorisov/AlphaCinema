using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class ChooseMovie : DisplayBaseCommand
    {
		private readonly IMovieGenreServices movieGenreServices;
		private readonly IMovieServices movieServices;

		public ChooseMovie(ICommandProcessor commandProcessor, IItemSelector selector, 
            IMovieGenreServices movieGenreServices, IMovieServices movieServices)
			: base(commandProcessor, selector)
        {
			this.movieGenreServices = movieGenreServices;
			this.movieServices = movieServices;
		}

		public override void Execute(List<string> parameters)
		{
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
			var genreID = parameters[1];
			var cityID = parameters[3];
			//var movieNames = this.movieGenreServices.GetMovieNamesByGenreIDCityID(genreID, cityID);
            // Ибираме Филм на база на жанра и града
            var movieNames = this.movieServices.GetMovieNamesByCityIDGenreID(genreID,cityID);
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
