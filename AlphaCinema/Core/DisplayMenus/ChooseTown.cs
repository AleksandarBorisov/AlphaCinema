using System.Collections.Generic;
using System.Linq;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;

namespace AlphaCinema.Core.DisplayMenus
{
    class ChooseTown : DisplayBaseCommand
    {
		public ChooseTown(ICommandProcessor commandProcessor, IItemSelector selector, ICityServices cityServices)
			: base(commandProcessor, selector, cityServices)
        {
		}

		public override void Execute(List<string> parameters)
		{
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
			//Тук ще направим заявка до базата от таблицата MovieProjections да ни даде GUID-овете на градовете за текущия ден
			var guids = cityServices.GetId();
			//Тук ще направим заявка до базата от таблицата Towns за да ни мапне филмите на GUID-овете
			List<string> towns = new List<string>() { "Vetren", "Pazardjik", "Dimitrovgrad" };
            List<string> displayItems = new List<string>() { "ChooseTown"};
            displayItems.AddRange(towns);
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
                parameters.Insert(0, guids[towns.IndexOf(result)].ToString());
                parameters.Insert(0, "ChooseMovie");
                commandProcessor.ExecuteCommand(parameters);
            }
        }
    }
}
