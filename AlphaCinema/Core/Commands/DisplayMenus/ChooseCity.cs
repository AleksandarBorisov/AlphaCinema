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

		public ChooseCity(ICommandProcessor commandProcessor, IItemSelector selector, ICityServices cityServices)
			: base(commandProcessor, selector)
        {
			this.cityServices = cityServices;
		}

		public override void Execute(List<string> parameters)
		{
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
                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
            }
			else
            {
                var cityID = this.cityServices.GetID(cityName);
                parameters.Insert(0, cityID.ToString());
                parameters.Insert(0, "ChooseGenre");
                commandProcessor.ExecuteCommand(parameters);
            }
        }
    }
}
