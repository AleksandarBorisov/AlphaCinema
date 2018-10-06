using System.Collections.Generic;
using System.Linq;
using AlphaCinema.Core.Contracts;

namespace AlphaCinema.Core.DisplayMenus
{
    class ChooseTown : ICommand
    {
        private ICommandProcessor commandProcessor;
        private IItemSelector selector;

        public ChooseTown(ICommandProcessor commandProcessor, IItemSelector selector)
        {
            this.commandProcessor = commandProcessor;
            this.selector = selector;
        }

        public void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            //Тук ще направим заявка до базата от таблицата MovieProjections да ни даде GUID-овете на градовете за текущия ден
            List<int> guids = new List<int>() { 123, 456, 789 };
            //Тук ще направим заявка до базата от таблицата Towns за да ни мапне филмите на GUID-овете
            List<string> towns = new List<string>() { "Vetren", "Pazardjik", "Dimitrovgrad" };
            List<string> displayItems = new List<string>() { "ChooseTown"};
            displayItems.AddRange(towns);
            displayItems.Add("Back");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);
            string result = selector.DisplayItems(displayItems);
            if (displayItems.IndexOf(result) == displayItems.Count - 3)
            {
                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
            }//Изтриваме командата ChoooseMovie и извикваме отново предното menu
            else
            {
                parameters.Insert(0, guids[towns.IndexOf(result)].ToString());
                parameters.Insert(0, "ChooseMovie");
                commandProcessor.ExecuteCommand(parameters);
            }
        }
    }
}
