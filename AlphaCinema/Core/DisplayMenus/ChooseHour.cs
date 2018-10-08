using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);
            string result = selector.DisplayItems(displayItems);
            while (result != "Back")
            {
                //Database.Add(guids[hours.IndexOf(result)].ToString(), townGuid, movieGuid)
                //Тук ще направим заявка към базата и ще добавим билета
                displayItems.Insert(displayItems.Count - 2,$"Your Reservation for {result} has been Added");
                result = selector.DisplayItems(displayItems);
                displayItems.RemoveAt(displayItems.Count - 3);
            }
            commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
        }
    }
}
