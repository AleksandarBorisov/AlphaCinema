using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class ChooseMovie : DisplayBaseCommand
    {
        private readonly IMovieServices movieServices;

        public ChooseMovie(IItemSelector selector, IMovieServices movieServices)
            : base(selector)
        {
            this.movieServices = movieServices;
        }

        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            var parameters = input.ToList();
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];

            int genreID = int.Parse(parameters[1]);
            int cityID = int.Parse(parameters[3]);

            // Избираме Филм на база на Жанра и Града
            var movieNames = this.movieServices.GetMovieNamesByCityIDGenreID(genreID, cityID);

            List<string> displayItems = new List<string>() { "Choose Movie" };

            displayItems.AddRange(movieNames);
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);

            string movieName = selector.DisplayItems(displayItems);
            if (movieName == "Back")
            {
                return parameters.Skip(2);
            }
            else if (movieName == "Home")
            {
                return parameters.Skip(5);
            }
            else
            {
                var movieID = this.movieServices.GetID(movieName);
                parameters.Insert(0, movieID.ToString());
                parameters.Insert(0, "ChooseHour");//Тук се налага да напишем командата ръчно
                return parameters;
            }
        }
    }
}