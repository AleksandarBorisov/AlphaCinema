using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
	public class ChooseHour : DisplayBaseCommand
	{
		private readonly IOpenHourServices openHourServices;
		private readonly IProjectionsServices projectionsServices;

		public ChooseHour(IItemSelector selector, IOpenHourServices openHourServices, IProjectionsServices projectionsServices)
			: base(selector)
		{
			this.openHourServices = openHourServices;
			this.projectionsServices = projectionsServices;
		}

		public override IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
            string offSetFromTop = parameters[parameters.Count - 2];
			string startingRow = parameters[parameters.Count - 1];

            int cityID = int.Parse(parameters[5]);
			int movieID = int.Parse(parameters[1]);
            
            //Избираме час на база на филма и града
			var hours = this.projectionsServices.GetOpenHoursByMovieIDCityID(movieID, cityID);

            List<string> displayItems = new List<string>() { "ChooseHour" };

			displayItems.AddRange(hours);
			displayItems.Add("Back");
			displayItems.Add("Home");
			displayItems.Add(offSetFromTop);
			displayItems.Add(startingRow);

            var startHour = selector.DisplayItems(displayItems);
			if (startHour == "Back")
			{
                return parameters.Skip(2);
			}
			else if (startHour == "Home")
			{
                return parameters.Skip(7);
			}
			else
			{
				var openHourID = openHourServices.GetID(startHour);
				parameters.Insert(0, openHourID.ToString());
				parameters.Insert(0, "EnterUser");
                return parameters;
			}
		}
	}
}