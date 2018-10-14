using System.Collections.Generic;
using System.Linq;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
	public class ChooseCity : DisplayBaseCommand
    {
		private readonly ICityServices cityServices;

		public ChooseCity(IItemSelector selector, ICityServices cityServices)
			: base(selector)
        {
			this.cityServices = cityServices;
		}

		public override IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];

			var cityNames = this.cityServices.GetCityNames();
            List<string> displayItems = new List<string>() { "Choose City"};

            displayItems.AddRange(cityNames);
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);

            string cityName = selector.DisplayItems(displayItems);

			if (cityName == "Back" || cityName == "Home")
            {
                return parameters.Skip(1);
            }
			else
            {
                var cityID = this.cityServices.GetID(cityName);
                parameters.Insert(0, cityID.ToString());
                parameters.Insert(0, "ChooseGenre");
                return parameters;
            }
        }
    }
}
