using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.DisplayMenus
{
    public class ChooseHour : DisplayBaseCommand
	{ 
        public ChooseHour(ICommandProcessor commandProcessor, IItemSelector selector)
			: base(commandProcessor, selector)
		{
			// Add choose hour service dependency
        }

		public override void Execute(List<string> parameters)
		{
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];

            string townGuid = parameters[3];
            string movieGuid = parameters[3];
            
            //Тук ще направим заявка до базата от таблицата MovieProjections да ни даде GUID-овете Прожекциите на филмите за текущия ден в текущия град
            List<int> guids = new List<int>() {123, 456, 789 };
            
            //Тук ще направим заявка до базата от таблицата Movies за да ни мапне Прожекциите на GUID-овете
            List<string> hours = new List<string> { "14:40h", "17:30h", "20:00h" };

            List<string> displayItems = new List<string>() { "ChooseHour"};

            displayItems.AddRange(hours);
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);

            string result = selector.DisplayItems(displayItems);
            while (result != "Back" && result != "Home")
            {
                //Database.Add(guids[hours.IndexOf(result)].ToString(), townGuid, movieGuid)
                //Тук ще направим заявка към базата и ще добавим билета
                selector.PrintAtPosition($"Your Reservation for {result} has been Added", (displayItems.Count - 2) * int.Parse(startingRow) + int.Parse(offSetFromTop), false);

                result = selector.DisplayItems(displayItems);
            }
            if (result == "Back")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
            }
            else if (result == "Home")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(6).ToList());
            }
        }
    }
}
