using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.DisplayMenus
{
    public class ChooseMovie : ICommand
    {
        private ICommandProcessor commandProcessor;
        private IItemSelector selector;

        public ChooseMovie(ICommandProcessor commandProcessor, IItemSelector selector)
        {
            this.commandProcessor = commandProcessor;
            this.selector = selector;
        }

        public void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            string townGuid = parameters[1];
            //Тук ще направим заявка до базата от таблицата MovieProjections да ни даде GUID-овете на филмите за текущия ден в текущия град
            List<int> guids = new List<int>() { 123, 456, 789 };
            //Тук ще направим заявка до базата от таблицата Movies за да ни мапне филмите на GUID-овете
            List<string> movies = new List<string>() { "Marvels Avenger's", "Two girls One cup", "Titanic" };
            List<string> displayItems = new List<string>() { "ChooseMovie"};
            displayItems.AddRange(movies);
            displayItems.Add("Back");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);
            string result = selector.DisplayItems(displayItems);
            if (displayItems.IndexOf(result) == displayItems.Count - 3)
            {
                commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
            }
            else
            {
                parameters.Insert(0, guids[movies.IndexOf(result)].ToString());
                parameters.Insert(0, "ChooseHour");//Тук се налага да напишем командата ръчно
                commandProcessor.ExecuteCommand(parameters);
            }

        }
    }
}
