using System.Collections.Generic;
using System.Linq;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;

namespace AlphaCinema.Core.DisplayMenus
{
    class ChooseTown : DisplayBaseCommand
    {
		private readonly ICityServices cityServices;

		public ChooseTown(ICommandProcessor commandProcessor, IItemSelector selector, ICityServices cityServices)
			: base(commandProcessor, selector)
        {
			this.cityServices = cityServices;
		}

		public override void Execute(List<string> parameters)
		{
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
			//Тук ще направим заявка до базата от таблицата MovieProjections да ни даде GUID-овете на градовете за текущия ден
			var cityIDs = this.cityServices.GetIDs();
			//Тук ще направим заявка до базата от таблицата Towns за да ни мапне филмите на GUID-овете
			List<string> cityNames = this.cityServices.GetCityNames(cityIDs);
            List<string> displayItems = new List<string>() { "ChooseTown"};
            displayItems.AddRange(cityNames);
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);
            string result = selector.DisplayItems(displayItems);
            if (result == "Back")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
            }//Изтриваме командата ChoooseMovie и извикваме отново предното menu
            else if (result == "Home")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
            }
            else
            {
                parameters.Insert(0, cityIDs[cityNames.IndexOf(result)].ToString());
                parameters.Insert(0, "ChooseMovie");
                commandProcessor.ExecuteCommand(parameters);
            }
        }
    }
}
