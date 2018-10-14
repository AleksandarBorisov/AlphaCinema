using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
	public class ChooseGenre : DisplayBaseCommand
	{
		private readonly IGenreServices genreServices;
		private readonly ICityServices cityServices;

		public ChooseGenre(IItemSelector selector,
			IGenreServices genreServices, ICityServices cityServices)
			: base(selector)
		{
			this.genreServices = genreServices;
			this.cityServices = cityServices;

		}

		public override IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            string offSetFromTop = parameters[parameters.Count - 2];
			string startingRow = parameters[parameters.Count - 1];

            int cityID = int.Parse(parameters[1]);
			
            // Избираме Жанр на база на Града
			var genreNames = this.cityServices.GetGenreNames(cityID);

            List<string> displayItems = new List<string>() { "Choose Genre" };

			displayItems.AddRange(genreNames);
			displayItems.Add("Back");
			displayItems.Add("Home");
			displayItems.Add(offSetFromTop);
			displayItems.Add(startingRow);

			string genreName = selector.DisplayItems(displayItems);
			if (genreName == "Back")
			{
                return parameters.Skip(2);
			}
			//Изтриваме командата ChoooseMovie и извикваме отново предното menu
			else if (genreName == "Home")
			{
                return parameters.Skip(3);
			}
			else
			{
				var genreID = this.genreServices.GetID(genreName);
				parameters.Insert(0, genreID.ToString());
				parameters.Insert(0, "ChooseMovie");
                return parameters;
			}
		}
	}
}